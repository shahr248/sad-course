using System;
using System.Windows.Forms;

namespace PEDIS
{
    public partial class PrisonerPanel : UserControl
    {
        public delegate void BackHandler();
        public event BackHandler onBack;

        private enum PrisonerFilterMode { All, ActiveInmates, PresentToday, ExpiringSafetyTraining }

        private DepartmentManagement currentUser;
        private PrisonerFilterMode currentFilter = PrisonerFilterMode.All;

        public PrisonerPanel()
        {
            InitializeComponent();
        }

        public void setCurrentUser(DepartmentManagement user)
        {
            this.currentUser = user;
        }

        private void PrisonerPanel_Load(object sender, EventArgs e)
        {
            // Factory Managers can only ever place a prisoner into the one factory they manage.
            bool isFactoryManager = currentUser != null && currentUser.getRole() == DepartmentManagementRole.FactoryManager;

            cmbAssignFactory.Items.Clear();
            foreach (Factory factory in Enum.GetValues(typeof(Factory)))
            {
                if (isFactoryManager && factory != currentUser.getFactory())
                    continue;

                cmbAssignFactory.Items.Add(factory);
            }
            if (cmbAssignFactory.Items.Count > 0)
                cmbAssignFactory.SelectedIndex = 0;

            refreshList();
            updateAvailableActions();
        }

        private void lvPrisoners_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateAvailableActions();
        }

        private Prisoner getSelectedPrisoner()
        {
            if (lvPrisoners.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a prisoner first.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            return (Prisoner)lvPrisoners.SelectedItems[0].Tag;
        }

        // Buttons are never created at runtime — only their Visible flag is toggled here,
        // based on which transitions are valid for the selected prisoner's current status.
        private void updateAvailableActions()
        {
            btnApprove.Visible = false;
            btnReject.Visible = false;
            btnAssignToFactory.Visible = false;
            cmbAssignFactory.Visible = false;
            btnStartProfessionalTraining.Visible = false;
            btnCompleteProfessionalTraining.Visible = false;
            btnStartSafetyTraining.Visible = false;
            btnCompleteSafetyTraining.Visible = false;
            btnSuspend.Visible = false;
            txtSuspendReason.Visible = false;
            btnReactivate.Visible = false;
            btnRelease.Visible = false;
            btnClockIn.Visible = false;
            btnClockOut.Visible = false;

            if (lvPrisoners.SelectedItems.Count == 0)
                return;

            Prisoner prisoner = (Prisoner)lvPrisoners.SelectedItems[0].Tag;
            if (prisoner == null)
                return;

            // Clock In/Out visibility is decoupled from ActivityStatus — driven only by
            // whether the prisoner already has an open attendance record for today.
            if (prisoner.getActivityStatus() != PrisonerActivityStatus.Archived)
            {
                bool hasOpenShiftToday = Program.AttendanceRecords.Exists(a =>
                    a.getPrisonerId() == prisoner.getId() &&
                    a.getAttendanceDate().Date == DateTime.Today &&
                    a.getEntryTime().HasValue &&
                    !a.getExitTime().HasValue);

                btnClockIn.Visible = !hasOpenShiftToday;
                btnClockOut.Visible = hasOpenShiftToday;

                // Release is available to any manager from any non-terminal status, not just
                // TemporarilyUnavailable — it's a deliberate override of the normal state machine.
                btnRelease.Visible = true;
            }

            switch (prisoner.getActivityStatus())
            {
                case PrisonerActivityStatus.PendingPrisonAdministrationApproval:
                case PrisonerActivityStatus.PendingDepartmentManagerApproval:
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
                    break;

                case PrisonerActivityStatus.PendingPlacement:
                    btnAssignToFactory.Visible = true;
                    cmbAssignFactory.Visible = true;
                    break;

                case PrisonerActivityStatus.Idle:
                    btnStartProfessionalTraining.Visible = true;
                    btnStartSafetyTraining.Visible = true;
                    btnSuspend.Visible = true;
                    txtSuspendReason.Visible = true;
                    break;

                case PrisonerActivityStatus.OnShiftWorking:
                case PrisonerActivityStatus.WaitingForMaterials:
                    btnSuspend.Visible = true;
                    txtSuspendReason.Visible = true;
                    break;

                case PrisonerActivityStatus.InProfessionalTraining:
                    btnCompleteProfessionalTraining.Visible = true;
                    btnSuspend.Visible = true;
                    txtSuspendReason.Visible = true;
                    break;

                case PrisonerActivityStatus.InSafetyTraining:
                    btnCompleteSafetyTraining.Visible = true;
                    btnSuspend.Visible = true;
                    txtSuspendReason.Visible = true;
                    break;

                case PrisonerActivityStatus.TemporarilyUnavailable:
                    btnReactivate.Visible = true;
                    break;

                case PrisonerActivityStatus.Archived:
                    // terminal state — no actions available
                    break;
            }
        }

        // Reselects the same prisoner by id after refreshList() rebuilds the ListView items
        // (Clear()/Add() loses selection), so updateAvailableActions() reflects the new status.
        private void refreshListAndReselect(int prisonerId)
        {
            refreshList();
            foreach (ListViewItem item in lvPrisoners.Items)
            {
                Prisoner p = (Prisoner)item.Tag;
                if (p.getId() == prisonerId)
                {
                    item.Selected = true;
                    item.EnsureVisible();
                    break;
                }
            }
            updateAvailableActions();
        }

        private bool matchesFilter(Prisoner prisoner)
        {
            switch (currentFilter)
            {
                case PrisonerFilterMode.ActiveInmates:
                    return prisoner.getActivityStatus() != PrisonerActivityStatus.Archived;
                case PrisonerFilterMode.PresentToday:
                    return Program.AttendanceRecords.Exists(a =>
                        a.getPrisonerId() == prisoner.getId() && a.getAttendanceDate().Date == DateTime.Today);
                case PrisonerFilterMode.ExpiringSafetyTraining:
                    return prisoner.getSafetyTrainingValidity().HasValue &&
                        prisoner.getSafetyTrainingValidity().Value.Date <= DateTime.Today.AddMonths(1);
                default:
                    return true;
            }
        }

        private void refreshList()
        {
            // Self-heal any prisoner stuck at OnShiftWorking with no active attendance
            // record for today before rendering, so the grid never shows a stale status.
            Prisoner.reconcileOnShiftStatuses();

            lvPrisoners.Items.Clear();
            foreach (Prisoner prisoner in Program.Prisoners)
            {
                // Factory Manager filtering: only show prisoners from their assigned factory
                if (currentUser != null && currentUser.getRole() == DepartmentManagementRole.FactoryManager)
                {
                    if (prisoner.getFactory() != currentUser.getFactory())
                        continue;
                }

                if (!matchesFilter(prisoner))
                    continue;

                ListViewItem item = new ListViewItem(prisoner.getPrisonerNumber());
                item.SubItems.Add(prisoner.getFullName());
                item.SubItems.Add(prisoner.getFactory()?.ToString() ?? "");
                item.SubItems.Add(prisoner.getDepartment().ToString());
                item.SubItems.Add(prisoner.getActivityStatus().ToString());
                item.SubItems.Add(prisoner.getRole()?.ToString() ?? "");
                item.SubItems.Add(prisoner.getHourlyRate().ToString("C"));
                item.SubItems.Add(prisoner.getWorkStartDate()?.ToString("yyyy-MM-dd") ?? "");
                item.SubItems.Add(prisoner.getSafetyTrainingValidity()?.ToString("yyyy-MM-dd") ?? "");
                item.SubItems.Add(prisoner.getReleaseDate()?.ToString("yyyy-MM-dd") ?? "");
                item.SubItems.Add(prisoner.getQualified() ? "Yes" : "No");
                item.Tag = prisoner;
                lvPrisoners.Items.Add(item);
            }
        }

        private void btnFilterAll_Click(object sender, EventArgs e)
        {
            currentFilter = PrisonerFilterMode.All;
            refreshList();
            updateAvailableActions();
        }

        private void btnFilterActive_Click(object sender, EventArgs e)
        {
            currentFilter = PrisonerFilterMode.ActiveInmates;
            refreshList();
            updateAvailableActions();
        }

        private void btnFilterPresentToday_Click(object sender, EventArgs e)
        {
            currentFilter = PrisonerFilterMode.PresentToday;
            refreshList();
            updateAvailableActions();
        }

        private void btnFilterExpiringSafety_Click(object sender, EventArgs e)
        {
            currentFilter = PrisonerFilterMode.ExpiringSafetyTraining;
            refreshList();
            updateAvailableActions();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (lvPrisoners.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a prisoner to view", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            Prisoner prisoner = (Prisoner)lvPrisoners.SelectedItems[0].Tag;
            string info = "Number: " + prisoner.getPrisonerNumber() + "\n" +
                         "Name: " + prisoner.getFullName() + "\n" +
                         "Factory: " + (prisoner.getFactory()?.ToString() ?? "") + "\n" +
                         "Status: " + prisoner.getActivityStatus() + "\n" +
                         "Role: " + (prisoner.getRole()?.ToString() ?? "") + "\n" +
                         "Hourly Rate: " + prisoner.getHourlyRate().ToString("C") + "\n" +
                         "Qualified: " + (prisoner.getQualified() ? "Yes" : "No");
            MessageBox.Show(info, "Prisoner Details", MessageBoxButtons.OK);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditPrisonerDialog dialog = new AddEditPrisonerDialog();
            dialog.setCurrentUser(currentUser);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                refreshList();
                updateAvailableActions();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvPrisoners.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a prisoner to edit", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            Prisoner prisoner = (Prisoner)lvPrisoners.SelectedItems[0].Tag;
            AddEditPrisonerDialog dialog = new AddEditPrisonerDialog();
            dialog.setCurrentUser(currentUser);
            dialog.setPrisonerToEdit(prisoner);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                refreshListAndReselect(prisoner.getId());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvPrisoners.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a prisoner to delete", "Selection Required", MessageBoxButtons.OK);
                return;
            }

            Prisoner prisoner = (Prisoner)lvPrisoners.SelectedItems[0].Tag;
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete prisoner: " + prisoner.getFullName() + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                prisoner.delete();
                refreshList();
                updateAvailableActions();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            onBack?.Invoke();
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = getSelectedPrisoner();
            if (prisoner == null)
                return;

            try
            {
                if (prisoner.getActivityStatus() == PrisonerActivityStatus.PendingPrisonAdministrationApproval)
                    prisoner.approvePrison();
                else if (prisoner.getActivityStatus() == PrisonerActivityStatus.PendingDepartmentManagerApproval)
                    prisoner.approveDeptManager();
                else
                {
                    MessageBox.Show("This prisoner is not in a pending-approval status.", "Action Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Prisoner approved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshListAndReselect(prisoner.getId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = getSelectedPrisoner();
            if (prisoner == null)
                return;

            try
            {
                if (prisoner.getActivityStatus() == PrisonerActivityStatus.PendingPrisonAdministrationApproval)
                    prisoner.rejectPrison();
                else if (prisoner.getActivityStatus() == PrisonerActivityStatus.PendingDepartmentManagerApproval)
                    prisoner.rejectDeptManager();
                else
                {
                    MessageBox.Show("This prisoner is not in a pending-approval status.", "Action Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Prisoner rejected and archived.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshListAndReselect(prisoner.getId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAssignToFactory_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = getSelectedPrisoner();
            if (prisoner == null)
                return;

            if (cmbAssignFactory.SelectedItem == null)
            {
                MessageBox.Show("Please select a factory.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Factory factory = (Factory)cmbAssignFactory.SelectedItem;
                prisoner.assignToFactory(factory);
                MessageBox.Show("Prisoner assigned to factory successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshListAndReselect(prisoner.getId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStartProfessionalTraining_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = getSelectedPrisoner();
            if (prisoner == null)
                return;

            try
            {
                prisoner.enrollInProfessionalTraining();
                MessageBox.Show("Prisoner enrolled in professional training.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshListAndReselect(prisoner.getId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCompleteProfessionalTraining_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = getSelectedPrisoner();
            if (prisoner == null)
                return;

            try
            {
                prisoner.completeProfessionalTraining();
                MessageBox.Show("Professional training completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshListAndReselect(prisoner.getId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStartSafetyTraining_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = getSelectedPrisoner();
            if (prisoner == null)
                return;

            try
            {
                prisoner.enrollInSafetyTraining();
                MessageBox.Show("Prisoner enrolled in safety training.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshListAndReselect(prisoner.getId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCompleteSafetyTraining_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = getSelectedPrisoner();
            if (prisoner == null)
                return;

            try
            {
                prisoner.completeSafetyTraining();
                MessageBox.Show("Safety training completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshListAndReselect(prisoner.getId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuspend_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = getSelectedPrisoner();
            if (prisoner == null)
                return;

            try
            {
                string reason = string.IsNullOrWhiteSpace(txtSuspendReason.Text) ? null : txtSuspendReason.Text.Trim();
                prisoner.placeOnHold(reason);
                txtSuspendReason.Text = "";
                MessageBox.Show("Prisoner placed on hold.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshListAndReselect(prisoner.getId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReactivate_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = getSelectedPrisoner();
            if (prisoner == null)
                return;

            try
            {
                prisoner.releaseFromHold();
                MessageBox.Show("Prisoner reactivated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshListAndReselect(prisoner.getId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = getSelectedPrisoner();
            if (prisoner == null)
                return;

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to permanently release this prisoner?",
                "Confirm Release",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            PasswordConfirmationDialog passwordDialog = new PasswordConfirmationDialog();
            if (passwordDialog.ShowDialog() != DialogResult.OK)
                return;

            if (passwordDialog.EnteredPassword != currentUser.getPassword())
            {
                MessageBox.Show("Incorrect password. Release cancelled.", "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                prisoner.archive();
                MessageBox.Show("Prisoner released and archived.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshListAndReselect(prisoner.getId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClockIn_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = getSelectedPrisoner();
            if (prisoner == null)
                return;

            if (!prisoner.getFactory().HasValue)
            {
                MessageBox.Show("Prisoner has no assigned factory. Assign a factory before clocking in.", "Action Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DateTime now = DateTime.Now;
                TimeSpan newEntry = now.TimeOfDay;

                foreach (AttendanceRecord record in Program.AttendanceRecords)
                {
                    if (record.getPrisonerId() != prisoner.getId() || record.getAttendanceDate().Date != now.Date)
                        continue;

                    TimeSpan existingEntry = record.getEntryTime() ?? TimeSpan.Zero;
                    TimeSpan existingExit = record.getExitTime() ?? TimeSpan.FromHours(24);

                    if (existingEntry <= newEntry && newEntry < existingExit)
                        throw new Exception("A shift is already reported for this prisoner at this time.");
                }

                int maxId = 0;
                foreach (AttendanceRecord record in Program.AttendanceRecords)
                {
                    if (record.getId() > maxId)
                        maxId = record.getId();
                }
                int nextId = maxId + 1;

                new AttendanceRecord(nextId, prisoner.getId(), now.Date, prisoner.getFactory().Value, newEntry, null, true);

                MessageBox.Show("Clocked in at " + now.ToString("HH:mm:ss") + ".", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshListAndReselect(prisoner.getId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClockOut_Click(object sender, EventArgs e)
        {
            Prisoner prisoner = getSelectedPrisoner();
            if (prisoner == null)
                return;

            try
            {
                DateTime now = DateTime.Now;
                AttendanceRecord openRecord = null;
                foreach (AttendanceRecord record in Program.AttendanceRecords)
                {
                    if (record.getPrisonerId() == prisoner.getId() &&
                        record.getAttendanceDate().Date == now.Date &&
                        record.getEntryTime().HasValue &&
                        !record.getExitTime().HasValue)
                    {
                        openRecord = record;
                        break;
                    }
                }

                if (openRecord == null)
                    throw new Exception("No open shift found for this prisoner today.");

                if (now.TimeOfDay <= openRecord.getEntryTime().Value)
                    throw new Exception("Clock out time must be after clock in time.");

                openRecord.setExitTime(now.TimeOfDay);
                openRecord.update();

                MessageBox.Show("Clocked out at " + now.ToString("HH:mm:ss") + ".", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refreshListAndReselect(prisoner.getId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Action Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
