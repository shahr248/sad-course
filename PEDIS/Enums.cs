using System;

namespace PEDIS
{
    public enum Factory
    {
        MaimonSpices,
        Technosak,
        SewingWorkshop,
        TzitzitWorkshop
    }

    public enum PrisonerActivityStatus
    {
        PendingPrisonAdministrationApproval,
        PendingDepartmentManagerApproval,
        PendingPlacement,
        Idle,
        OnShiftWorking,
        WaitingForMaterials,
        InProfessionalTraining,
        InSafetyTraining,
        TemporarilyUnavailable,
        Archived
    }

    public enum PrisonerRole
    {
        Supervisor,
        General
    }

    public enum DepartmentManagementRole
    {
        DepartmentManager,
        DeputyOfDepartmentManager,
        FactoryManager
    }

    public enum ContractStatus
    {
        Active,
        Inactive,
        Expired
    }

    public enum ProductionOrderStatus
    {
        Recieved,
        InProduction,
        ReadyForPickup,
        Delivered,
        Cancelled,
        OnHold
    }

    public enum WorkOrderStatus
    {
        InProcess,
        Completed,
        HasntEnteredIntoProductionYet
    }

    public enum ProductivityType
    {
        Individual,
        Group
    }

    public enum UnitOfMeasure
    {
        Kg,
        Gr,
        Units
    }

    // =====================================================================
    // Helper classes for enum conversions
    // =====================================================================

    public static class EnumHelpers
    {
        public static string ToDbString(Factory factory)
        {
            return factory switch
            {
                Factory.MaimonSpices => "MaimonSpices",
                Factory.Technosak => "Technosak",
                Factory.SewingWorkshop => "SewingWorkshop",
                Factory.TzitzitWorkshop => "TzitzitWorkshop",
                _ => "MaimonSpices"
            };
        }

        public static Factory FactoryFromDb(string value)
        {
            return value switch
            {
                "MaimonSpices" => Factory.MaimonSpices,
                "Technosak" => Factory.Technosak,
                "SewingWorkshop" => Factory.SewingWorkshop,
                "TzitzitWorkshop" => Factory.TzitzitWorkshop,
                _ => Factory.MaimonSpices
            };
        }

        public static string ToDbString(PrisonerActivityStatus status)
        {
            return status switch
            {
                PrisonerActivityStatus.PendingPrisonAdministrationApproval => "pendingPrisonAdministrationApproval",
                PrisonerActivityStatus.PendingDepartmentManagerApproval => "pendingDepartmentManagerApproval",
                PrisonerActivityStatus.PendingPlacement => "pendingPlacement",
                PrisonerActivityStatus.Idle => "idle",
                PrisonerActivityStatus.OnShiftWorking => "onShiftWorking",
                PrisonerActivityStatus.WaitingForMaterials => "waitingForMaterials",
                PrisonerActivityStatus.InProfessionalTraining => "inProfessionalTraining",
                PrisonerActivityStatus.InSafetyTraining => "inSafetyTraining",
                PrisonerActivityStatus.TemporarilyUnavailable => "temporarilyUnavailable",
                PrisonerActivityStatus.Archived => "archived",
                _ => "idle"
            };
        }

        public static PrisonerActivityStatus ActivityStatusFromDb(string value)
        {
            return value switch
            {
                "pendingPrisonAdministrationApproval" => PrisonerActivityStatus.PendingPrisonAdministrationApproval,
                "pendingDepartmentManagerApproval" => PrisonerActivityStatus.PendingDepartmentManagerApproval,
                "pendingPlacement" => PrisonerActivityStatus.PendingPlacement,
                "idle" => PrisonerActivityStatus.Idle,
                "onShiftWorking" => PrisonerActivityStatus.OnShiftWorking,
                "waitingForMaterials" => PrisonerActivityStatus.WaitingForMaterials,
                "inProfessionalTraining" => PrisonerActivityStatus.InProfessionalTraining,
                "inSafetyTraining" => PrisonerActivityStatus.InSafetyTraining,
                "temporarilyUnavailable" => PrisonerActivityStatus.TemporarilyUnavailable,
                "archived" => PrisonerActivityStatus.Archived,
                _ => PrisonerActivityStatus.Idle
            };
        }

        public static string ToDbString(PrisonerRole role)
        {
            return role switch
            {
                PrisonerRole.Supervisor => "supervisor",
                PrisonerRole.General => "general",
                _ => "general"
            };
        }

        public static PrisonerRole PrisonerRoleFromDb(string value)
        {
            return value switch
            {
                "supervisor" => PrisonerRole.Supervisor,
                "general" => PrisonerRole.General,
                _ => PrisonerRole.General
            };
        }

        public static string ToDbString(DepartmentManagementRole role)
        {
            return role switch
            {
                DepartmentManagementRole.DepartmentManager => "departmentManager",
                DepartmentManagementRole.DeputyOfDepartmentManager => "deputyOfDepartmentManager",
                DepartmentManagementRole.FactoryManager => "factoryManager",
                _ => "factoryManager"
            };
        }

        public static DepartmentManagementRole DeptMgmtRoleFromDb(string value)
        {
            return value switch
            {
                "departmentManager" => DepartmentManagementRole.DepartmentManager,
                "deputyOfDepartmentManager" => DepartmentManagementRole.DeputyOfDepartmentManager,
                "factoryManager" => DepartmentManagementRole.FactoryManager,
                _ => DepartmentManagementRole.FactoryManager
            };
        }

        public static string ToDbString(ContractStatus status)
        {
            return status switch
            {
                ContractStatus.Active => "Active",
                ContractStatus.Inactive => "Inactive",
                ContractStatus.Expired => "Expired",
                _ => "Active"
            };
        }

        public static ContractStatus ContractStatusFromDb(string value)
        {
            return value switch
            {
                "Active" => ContractStatus.Active,
                "Inactive" => ContractStatus.Inactive,
                "Expired" => ContractStatus.Expired,
                _ => ContractStatus.Active
            };
        }

        public static string ToDbString(ProductionOrderStatus status)
        {
            return status switch
            {
                ProductionOrderStatus.Recieved => "recieved",
                ProductionOrderStatus.InProduction => "inProduction",
                ProductionOrderStatus.ReadyForPickup => "readyForPickup",
                ProductionOrderStatus.Delivered => "delivered",
                ProductionOrderStatus.Cancelled => "cancelled",
                ProductionOrderStatus.OnHold => "onHold",
                _ => "recieved"
            };
        }

        public static ProductionOrderStatus OrderStatusFromDb(string value)
        {
            return value switch
            {
                "recieved" => ProductionOrderStatus.Recieved,
                "inProduction" => ProductionOrderStatus.InProduction,
                "readyForPickup" => ProductionOrderStatus.ReadyForPickup,
                "delivered" => ProductionOrderStatus.Delivered,
                "cancelled" => ProductionOrderStatus.Cancelled,
                "onHold" => ProductionOrderStatus.OnHold,
                _ => ProductionOrderStatus.Recieved
            };
        }

        public static string ToDbString(WorkOrderStatus status)
        {
            return status switch
            {
                WorkOrderStatus.InProcess => "inProcess",
                WorkOrderStatus.Completed => "completed",
                WorkOrderStatus.HasntEnteredIntoProductionYet => "hasntEnteredIntoProductionYet",
                _ => "hasntEnteredIntoProductionYet"
            };
        }

        public static WorkOrderStatus WorkOrderStatusFromDb(string value)
        {
            return value switch
            {
                "inProcess" => WorkOrderStatus.InProcess,
                "completed" => WorkOrderStatus.Completed,
                "hasntEnteredIntoProductionYet" => WorkOrderStatus.HasntEnteredIntoProductionYet,
                _ => WorkOrderStatus.HasntEnteredIntoProductionYet
            };
        }

        public static string ToDbString(ProductivityType type)
        {
            return type switch
            {
                ProductivityType.Individual => "Individual",
                ProductivityType.Group => "Group",
                _ => "Individual"
            };
        }

        public static ProductivityType ProductivityTypeFromDb(string value)
        {
            return value switch
            {
                "Individual" => ProductivityType.Individual,
                "Group" => ProductivityType.Group,
                _ => ProductivityType.Individual
            };
        }

        public static string ToDbString(UnitOfMeasure unit)
        {
            return unit switch
            {
                UnitOfMeasure.Kg => "kg",
                UnitOfMeasure.Gr => "gr",
                UnitOfMeasure.Units => "units",
                _ => "units"
            };
        }

        public static UnitOfMeasure UnitOfMeasureFromDb(string value)
        {
            return value switch
            {
                "kg" => UnitOfMeasure.Kg,
                "gr" => UnitOfMeasure.Gr,
                "units" => UnitOfMeasure.Units,
                _ => UnitOfMeasure.Units
            };
        }
    }
}
