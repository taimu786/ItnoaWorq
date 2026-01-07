namespace ItnoaWorq.Domain.Enums;

public enum JobType : byte { FullTime = 1, PartTime = 2, Contract = 3, Intern = 4 }
public enum ApplicationStatus : byte { Applied = 1, Screening = 2, Interview = 3, Offer = 4, Hired = 5, Rejected = 6 }
public enum EmploymentStatus : byte { Active = 1, Probation = 2, Suspended = 3, Resigned = 4, Terminated = 5 }
public enum RequestType : byte { Leave = 1, Asset = 2, AdvanceSalary = 3, Access = 4 }
public enum RequestStatus : byte { Draft = 1, Pending = 2, Approved = 3, Rejected = 4, Cancelled = 5 }
public enum PayrollStatus : byte { Open = 1, Locked = 2, Processed = 3 }
public enum PayComponentType : byte { Earning = 1, Deduction = 2 }
public enum ReviewStatus : byte { Draft = 1, Submitted = 2, Final = 3 }

// NEW enums for Plans
public enum PlanCategory : byte { Personal = 1, Business = 2 }
public enum PlanType : byte { Free = 1, Paid = 2 }
public enum ReactionType : byte { Like = 1, Love = 2, Clap = 3, Laugh = 4, Insightful = 5 }
public enum ConnectionStatus { Pending = 0, Accepted = 1, Rejected = 2 }

