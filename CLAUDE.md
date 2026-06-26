# ItnoaWorq — Project Context for Claude

## What Is This?

ItnoaWorq is a SaaS platform that combines three things into one:
- **LinkedIn-style professional network** — profiles, connections, feed, posts
- **Job Portal** — job listings, applications, hiring pipeline
- **HR Management System (HRMS)** — employee records, attendance, payroll, leaves

**One-line pitch:** "LinkedIn + Job Portal + HR Software — all in one platform"

Target users:
- **Companies** — post jobs, hire candidates, manage employees
- **Job Seekers** — build profile, apply for jobs, network with professionals

**Business Model (Multi-tenant SaaS):**
| Plan | Features |
|---|---|
| Free | Job posting only |
| Basic | More visibility, basic HR |
| Premium | Full HRMS (payroll, attendance, leaves) |

---

## Tech Stack

### Backend
- ASP.NET Core 8 Web API
- Entity Framework Core + SQL Server
- ASP.NET Identity + JWT Authentication
- MediatR (CQRS pattern)
- Repository Pattern + Unit of Work
- FluentValidation
- Multi-tenancy via `ITenantProvider` / `HttpTenantProvider`

### Frontend
- React 19 + Vite + TypeScript
- Redux Toolkit (state management)
- React Router v7
- Axios (with Bearer token interceptor)
- Tailwind CSS v4
- react-hot-toast (notifications)

---

## Solution Structure

```
ItnoaWorq/
├── ItnoaWorq.Domain/          # Entities, Enums
├── ItnoaWorq.Application/     # CQRS Handlers, DTOs, Interfaces
├── ItnoaWorq.Infrastructure/  # EF Core, Repositories, JWT, Seeders
├── ItnoaWorq.Api/             # Controllers, Program.cs
└── itnoaworq-frontend/        # React + Vite frontend (PRIMARY)
```

> Note: `ItnoaWorq.Frontend222/` and `itnoaworq-angular/` are experimental — ignore them.

---

## Architecture Rules (ALWAYS follow these)

1. **Clean Architecture** — Domain → Application → Infrastructure → Api
2. **CQRS via MediatR** — every feature has Commands (write) and Queries (read)
3. **Repository + Unit of Work** — use `_uow.Repository<TEntity>()`, never direct DbContext in handlers
4. **All writes** committed via `await _uow.SaveChangesAsync(cancellationToken)`
5. **No business logic in controllers** — controllers only send MediatR requests
6. **Keep Program.cs clean** — registrations go in `ApplicationDI.cs` or `InfrastructureDI.cs`
7. **DTOs always** — never expose domain entities directly
8. **No comments** unless the WHY is non-obvious

### CQRS Folder Structure (per feature)
```
Features/
└── FeatureName/
    ├── Commands/       # CreateXCommand, UpdateXCommand, DeleteXCommand
    ├── Queries/        # GetXQuery, GetAllXQuery
    ├── Handlers/       # One handler per command/query
    └── Validators/     # FluentValidation validators
```

### Multi-Tenancy
- Tenant ID passed via request header: `X-Tenant: {tenantId}`
- EF Core Global Query Filter auto-applies tenant isolation
- Use `ITenantProvider` to get current tenant in handlers

---

## Domain Entities

### Core
| Entity | Description |
|---|---|
| `Tenant` | Company/organization using the platform |
| `Plan` | Subscription plan (Free/Basic/Premium) |

### Identity
| Entity | Description |
|---|---|
| `AppUser` | Extends IdentityUser — has FullName, PublicProfile nav |
| `AppRole` | Role-based access (SuperAdmin, Admin, HR, Employee, Public) |

### Profile
| Entity | Description |
|---|---|
| `PublicProfile` | User's LinkedIn-style profile (headline, summary, profession, location) |
| `Skill` | Global skill definitions |
| `ProfileSkill` | User's skill with proficiency level |
| `Education` | User's education history |
| `Experience` | User's work experience |

### Jobs
| Entity | Description |
|---|---|
| `Job` | Job posting (title, type, location, description, isActive) |
| `JobApplication` | Application record linking User → Job with status |

### Social / Feed
| Entity | Description |
|---|---|
| `Post` | Feed post content |
| `PostComment` | Comment on a post |
| `PostReaction` | Like/Love/Clap/Laugh/Insightful reaction |

### Networking
| Entity | Description |
|---|---|
| `Connection` | User-to-user connection (Pending/Accepted/Rejected) |

### HRMS
| Entity | Description |
|---|---|
| `Employee` | Employee record linked to Tenant and AppUser |
| `Department` | Company department |
| `AttendanceLog` | Clock in/out records |
| `Request` | Leave/Asset/AdvanceSalary/Access requests |
| `Payroll` | Monthly payroll record |
| `PayrollDetail` | Individual earning/deduction line items |
| `PerformanceReview` | Employee review record |

### Communication
| Entity | Description |
|---|---|
| `Message` | Direct message between users |
| `Notification` | User notification |
| `AuditLog` | Activity tracking |

### Base Entity
All entities inherit `BaseEntity`:
```csharp
public Guid Id { get; set; }
public Guid? TenantId { get; set; }  // null = platform-level
public DateTime CreatedAt { get; set; }
public DateTime? UpdatedAt { get; set; }
```

---

## Enums (ItnoaWorq.Domain/Enums/Enums.cs)

```csharp
JobType           { FullTime, PartTime, Contract, Intern }
ApplicationStatus { Applied, Screening, Interview, Offer, Hired, Rejected }
EmploymentStatus  { Active, Probation, Suspended, Resigned, Terminated }
RequestType       { Leave, Asset, AdvanceSalary, Access }
RequestStatus     { Pending, Approved, Rejected }
PayrollStatus     { Draft, Approved, Paid }
PayComponentType  { Earning, Deduction }
ReviewStatus      { Pending, Completed }
PlanCategory      { Free, Basic, Premium }
PlanType          { Monthly, Yearly }
ReactionType      { Like, Love, Clap, Laugh, Insightful }
ConnectionStatus  { Pending, Accepted, Rejected }
```

---

## Backend — What's Implemented

### Controllers & Endpoints

#### AuthController — `/api/auth`
| Method | Route | Description |
|---|---|---|
| POST | `/register` | Register user, assign role, return JWT |
| POST | `/login` | Login, return JWT |

#### ProfilesController — `/api/profiles` [Authorize]
| Method | Route | Description |
|---|---|---|
| GET | `/me` | Get current user's profile |
| PUT | `/me` | Update profile (headline, summary, profession, location) |
| PUT | `/me/skills` | Replace user's skill list |
| POST | `/me/education` | Add education entry |
| POST | `/me/experience` | Add experience entry |

#### PostsController — `/api/posts` [Authorize]
| Method | Route | Description |
|---|---|---|
| POST | `/` | Create post |
| GET | `/feed` | Paginated feed (connection posts + trending + random) |
| GET | `/{id}` | Get single post with comments/reactions |
| POST | `/{id}/comment` | Add comment |
| POST | `/{id}/reaction` | Add/change reaction (ReactionType enum) |
| GET | `/trending` | Trending posts |

#### JobsController — `/api/jobs`
| Method | Route | Description |
|---|---|---|
| GET | `/` | All active jobs (anonymous) |
| GET | `/{id}` | Job details (anonymous) |
| POST | `/{id}/apply` | Apply for job [Authorize] |
| POST | `/post` | Create job [HR/Admin] |
| GET | `/my-jobs` | Employer's own postings [Authorize] |
| GET | `/{id}/applicants` | Applicants list [Authorize] |
| PUT | `/applications/{id}/status` | Update application status [Authorize] |
| GET | `/applications` | Candidate's own applications [Authorize] |
| PUT | `/{id}` | Edit job [Authorize] |
| DELETE | `/{id}` | Delete job [Authorize] |

#### ConnectionsController — `/api/connections` [Authorize]
| Method | Route | Description |
|---|---|---|
| POST | `/{targetUserId}/request` | Send connection request |
| POST | `/{requestId}/accept` | Accept request |
| POST | `/{requestId}/reject` | Reject request |
| GET | `/pending` | Pending requests |
| GET | `/` | All connections |
| GET | `/suggestions` | Suggested connections |

#### TenantsController — `/api/tenants` [SuperAdmin]
| Method | Route | Description |
|---|---|---|
| POST | `/` | Create new tenant/organization |

### Seeders (run on startup)
- **IdentitySeed** — roles (SuperAdmin, Admin, HR, Employee, Public) + SuperAdmin user
- **PlanSeed** — Free/Basic/Premium plans + demo tenant

---

## Application Layer — Handlers Implemented

### Profiles
- `GetMyProfileHandler` → `GetMyProfileQuery`
- `UpdateMyProfileHandler` → `UpdateMyProfileCommand`
- `AddEducationHandler` → `AddEducationCommand`
- `AddExperienceHandler` → `AddExperienceCommand`
- `UpdateMySkillsHandler` → `UpdateMySkillsCommand`

### Posts
- `CreatePostHandler`, `GetFeedHandler` (complex algorithm), `GetPostByIdHandler`
- `AddCommentHandler`, `AddReactionHandler`, `GetTrendingPostsHandler`

### Jobs
- `PostJobHandler`, `ApplyForJobHandler`, `GetAllJobsHandler`, `GetMyJobsHandler`
- `GetJobByIdHandler`, `GetApplicantsForJobHandler`, `UpdateApplicationStatusHandler`
- `EditJobHandler`, `DeleteJobHandler`

### Connections
- `SendConnectionRequestHandler`, `AcceptConnectionHandler`, `RejectConnectionHandler`
- `GetConnectionsHandler`, `GetPendingRequestsHandler`, `GetConnectionSuggestionsHandler`

---

## Infrastructure

### Database
- SQL Server, connection string in `appsettings.json`
- Migrations in `ItnoaWorq.Infrastructure/Persistence/Migrations/`
- Design-time factory: `HrmsDbContextFactory`

### Key Files
```
Infrastructure/
├── Persistence/
│   ├── HrmsDbContext.cs           # DbContext with all DbSets + global tenant filter
│   └── Migrations/
├── Repositories/
│   ├── Repository.cs              # Generic CRUD: GetAllAsync, FindAsync, AddAsync, Update, Delete
│   └── UnitOfWork.cs              # SaveChangesAsync + Repository<T> factory
├── Security/
│   ├── JwtTokenService.cs         # JWT generation
│   └── JwtOptions.cs
├── Tenancy/
│   └── HttpTenantProvider.cs      # Reads X-Tenant header
└── Seeders/
    ├── IdentitySeed.cs
    └── PlanSeed.cs
```

---

## Frontend — Structure

**Location:** `itnoaworq-frontend/`
**API Base URL:** `https://localhost:7175/api/` (from `.env` → `VITE_API_BASE_URL`)

```
src/
├── main.tsx                  # Entry — Redux Provider
├── routes/AppRoutes.tsx      # React Router v7 routes + PrivateRoute guard
├── app/store.ts              # Redux store
│
├── pages/
│   ├── Auth/Login.tsx        # ✅ Login form
│   ├── Auth/Register.tsx     # ✅ Register form
│   ├── Feed/FeedPage.tsx     # ✅ Feed with post creation
│   ├── Profile/ProfilePage.tsx # ✅ Profile display (edit not wired up)
│   ├── Dashboard.tsx         # ⬜ Stub
│   ├── Jobs/JobsPage.tsx     # ⬜ Stub
│   └── Network/NetworkPage.tsx # ⬜ Stub
│
├── components/
│   ├── Layouts/MainLayout.tsx  # Responsive grid layout
│   ├── Layouts/Navbar.tsx      # Fixed top nav
│   ├── feed/CreatePostModal.tsx # ✅ Post creation modal
│   ├── feed/PostCard.tsx        # ✅ Post with like/comment
│   └── profile/                 # ✅ ProfileHeader, AboutSection, ExperienceSection, EducationSection
│
├── features/
│   ├── auth/authSlice.ts     # setAuth, logout — persists to localStorage
│   ├── auth/authService.ts   # loginApi, registerApi
│   ├── feed/feedSlice.ts     # fetchFeed, createPost, reactToPost, addComment
│   ├── feed/feedService.ts   # API calls
│   ├── feed/feedMapper.ts    # PostDto → Post mapping
│   ├── profile/profileSlice.ts # fetchProfile, updateProfile, addEducation, addExperience, updateSkills
│   └── profile/profileService.ts
│
├── types/
│   ├── auth.ts               # User, LoginRequest/Response, RegisterRequest/Response
│   ├── feed.ts               # Post, PostDto, CommentDto, ReactionDto, ReactionType
│   ├── profile.ts            # ProfileDto, SkillDto, EducationDto, ExperienceDto
│   └── common.ts             # ApiResponse<T>, PaginationParams
│
├── lib/axios.ts              # Axios instance + Bearer token interceptor
├── hooks/redux.ts            # useAppDispatch, useAppSelector (typed)
└── config/env.ts             # API_BASE_URL constant
```

### Routes
```
/login      → Login (public)
/register   → Register (public)
/dashboard  → Dashboard (PrivateRoute — needs auth)
/feed       → FeedPage
/network    → NetworkPage (stub)
/jobs       → JobsPage (stub)
/profile    → ProfilePage
```

---

## Implementation Status

### Phase 1 — MVP

| Feature | Backend | Frontend |
|---|---|---|
| Authentication | ✅ | ✅ |
| Public Profile (view) | ✅ | ✅ |
| Public Profile (edit) | ✅ | ⬜ not wired |
| Add Skills / Education / Experience | ✅ | ⬜ not wired |
| Feed (view/create/like/comment) | ✅ | ✅ |
| Jobs (browse & apply) | ✅ | ⬜ stub |
| Jobs (post & manage) | ✅ | ⬜ stub |
| Connections (send/accept/reject) | ✅ | ⬜ stub |
| Connection Suggestions | ✅ | ⬜ stub |
| Dashboard | — | ⬜ stub |

### Phase 2 — HRMS (entities exist, no handlers yet)
- Attendance tracking
- Leave requests
- Payroll
- Employee management
- Performance reviews

### Phase 3 — Communication
- Direct messaging (entity exists, no handler)
- Notifications (entity exists, no handler)

### Phase 4+
- Search / filtering
- Analytics & dashboards
- AI matching
- Mobile app (React Native)

---

## What's Missing / Known Issues

### Already Fixed
- ~~`AddEducationCommand.cs` had wrong record name (`ApplyForJobCommand`)~~
- ~~`AddEducationHandler` implemented wrong command~~
- ~~`GetMyProfileQuery.cs` had wrong record name (`GetJobByIdQuery`)~~
- ~~`GetMyProfileHandler` implemented wrong query~~
- ~~`ProfilesController` used wrong command/query types~~

### Still Pending
- No FluentValidation validators written for any command
- No global exception handling middleware in API
- Feed has no pagination on frontend (API supports it)
- Navbar search bar is UI-only, not wired to any API
- Profile edit buttons exist in UI but have no click handlers
- No loading skeletons (just spinner)
- `App.tsx` is still default Vite boilerplate (not used — `AppRoutes.tsx` is the real entry)
- No Redux slice for Jobs or Connections on frontend
- sweetalert2 is installed but unused

---

## Key Conventions

### Backend
- Handler constructor always injects `IUnitOfWork _uow`
- User ID from JWT: `User.FindFirstValue(ClaimTypes.NameIdentifier)`
- Tenant ID from: `ITenantProvider.GetTenantId()`
- Return types: commands → `Unit` or `Guid`, queries → DTO
- Register services: `ApplicationDI.cs` (MediatR, validators) and `InfrastructureDI.cs` (DbContext, repos, JWT)

### Frontend
- API calls go in `features/{module}/{module}Service.ts`
- Redux state goes in `features/{module}/{module}Slice.ts`
- Types go in `types/{module}.ts`
- Use `useAppDispatch` and `useAppSelector` from `hooks/redux.ts`
- Toast: `toast.success()` / `toast.error()` from react-hot-toast
- Tailwind v4 — no `tailwind.config.js` classes needed, use utility classes directly
