namespace DynamicWorkflow.Aggregator.Helpers.Enums
{
    public enum RequestType
    {
        Report,
        Service
    }
    public enum OperationStatus
    {
        Solved,
        Rejected,
        Closed,
        DeLay,
        Converted,
        Received,
        AllReports,
        Late,
        Continued,
        Requested,
        Accepted,
        ReOpen
    }
    public enum ConsentRequestStatus
    {
        Approved,
        Rejected,
        Wating
    }
    public enum State
    {
        NotDeleted,
        Deleted
    }
    public enum StatusEnum
    {
        FailedToSave,
        SavedSuccessfully,
        Exception,
        Success,
        Failed,
        FailedToFindTheObject,
        AlreadyExisting
    }
    public enum UserType
    {
        Local,
        Domain
    }
    public enum ActionNameEnum
    {
        Get,
        Post,
        Put,
        Delete,
    }
    public enum UserGender
    {
        Male,
        Female
    }
    public enum UserKind
    {
        Manager,
        Employee
    }
    public enum KafkaEvent
    {
        Post,
        Put,
        Delete
    }
    public enum MaritalStatus
    {
        Single,
        married
    }
    public enum NoteTypeId
    {
        SubSystem,
        Component,
        Part
    }
    public enum Color
    {
        Red,
        Green,
        Yellow,
    }
    public enum Table
    {
        User,
        Job,
        Card,
        Role
    }

    public enum TableColumn
    {
        UserTypeId = 1,
        JobDegreeId = 2,
        CardSetting = 3,
        CategoryRoleId = 4,
        UserJobTitle = 5,
        JobPositionId = 6

    }
    public enum JobTime
    {

        Basic, Responsible
    }

    public enum Priority
    {
        High = 1,
        Medium = 2,
        Low = 3
    }

    public enum CategoryStatus
    {
        Status1 = 1,
        Status2 = 2,
    }

    public enum Effect
    {
        None = 1,
    }

    public enum PriorityLevel
    {
        High = 1,
        Medium = 2,
        Low = 3
    }

    public enum ServiceType
    {
        All = 1,
        RequestService = 2,
        ReportService = 3
    }
}