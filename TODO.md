# Bypass Login - Admin Session Task

## Plan Details
- **Information Gathered**: App starts with FrmLogin. Admin logs in with 'admin'/'admin123' to access FrmAdministrador. Program.cs launches FrmLogin. No running app.
- **Plan**: Add CLI arg support in Program.cs to bypass login:
  - `dotnet run` : Normal login.
  - `dotnet run -- --admin-session` : Direct admin dashboard (FrmAdministrador).
- **Dependent Files**: Program.cs
- **Followup**: Build, run with arg to start admin session. Git/PR manual (gh not installed).

## Steps Progress
- [x] Plan confirmed & TODO created
- [x] Edit Program.cs
- [x] Test build/run
- [ ] Create branch blackboxai/bypass-login, commit, PR manual

Run `dotnet build` to verify.
