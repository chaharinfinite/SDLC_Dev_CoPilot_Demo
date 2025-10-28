# Patient Portal (.NET Framework)

## Solution Overview

- `src/PatientPortal.sln` – Visual Studio solution targeting .NET Framework 4.8.
- `PatientPortal.Domain` – Core domain entities, value objects, and enums modelling appointments, records, messaging, billing, telehealth, and security primitives.
- `PatientPortal.Application` – Application services, DTOs, and interfaces encapsulating business use-cases (registration, scheduling, messaging, medical records, prescriptions, billing, document uploads, education resources, notifications, telehealth, AI triage, device integration).
- `PatientPortal.Infrastructure` – In-memory persistence, console notification gateway, filesystem storage, rule-based AI triage client, and simplified identity provider adapters.
- `PatientPortal.Web` – ASP.NET MVC 5 front-end exposing JSON endpoints for all portal capabilities, wired up through a light dependency resolver.

## Key Features Implemented

- Secure user profile management with MFA & biometric flags, communication preferences, accessibility settings, and connected devices.
- Appointment lifecycle (book, reschedule, cancel, confirm) with collision detection and automated reminders.
- HIPAA-friendly secure messaging inbox with read/archive support and tech-support flagging.
- Comprehensive medical record access, including historical lab trends, document downloads, and education resources with accessibility filters.
- Prescription management with refill workflows and audit history.
- Billing portal providing statement visibility, integrated payments, receipts, and insurance breakdowns.
- Digital document intake (ID, insurance) with filesystem storage abstraction and verification workflow.
- Notification engine supporting email/SMS/push/in-portal reminders and delivery tracking.
- Telehealth session orchestration (virtual waiting room, monitoring devices, issue escalation).
- AI-assisted symptom triage for routing and virtual care prioritisation.
- Device integration services for wearables/IoT syncing.

## Getting Started

1. Open `src/PatientPortal.sln` in Visual Studio 2022 (or newer) with .NET Framework 4.8 workloads installed.
2. Restore any missing GAC assemblies (e.g., System.Web.Mvc 5.2.7) via Visual Studio installer if prompted.
3. Set `PatientPortal.Web` as the startup project and press **F5** to launch the MVC JSON endpoint surface (IIS Express).
4. Seed data via controller endpoints or extend `InfrastructureModule` to plug in a database/identity provider of your choice.

## Next Steps

- Replace in-memory repositories with Entity Framework 6 or Dapper-based persistence (SQL Server/Azure SQL).
- Integrate production identity (Azure AD B2C/Okta) with real MFA/biometric enrollment.
- Swap console notifications for Twilio/SendGrid/FCM gateways and queue background delivery.
- Implement full UI (Razor views/SPAs) and add localization plus accessibility tooling.
- Build automated tests around application services and controllers using MSTest or NUnit.
