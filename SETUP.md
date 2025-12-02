# Wintakam Setup Guide

This guide will help you set up and run the Wintakam app.

## Prerequisites

- .NET 9.0 SDK with MAUI workloads installed
- A Supabase account (free tier works fine)

## Setup Steps

### 1. Configure Supabase Credentials

The app needs your Supabase credentials to function. Follow these steps:

1. **Copy the example configuration file:**
   ```bash
   copy appsettings.example.json appsettings.json
   ```

   Or on Mac/Linux:
   ```bash
   cp appsettings.example.json appsettings.json
   ```

2. **Get your Supabase credentials:**
   - Go to your [Supabase Dashboard](https://supabase.com/dashboard)
   - Select your project (or create a new one)
   - Go to **Settings** (gear icon ⚙️) → **API**
   - Copy these values:
     - **Project URL** (example: `https://xxxxx.supabase.co`)
     - **anon public key** (starts with `eyJ...`)

3. **Edit `appsettings.json`:**
   Open the newly created `appsettings.json` file and replace the placeholder values:

   ```json
   {
     "Supabase": {
       "Url": "https://your-actual-project-id.supabase.co",
       "AnonKey": "your-actual-anon-key-here"
     }
   }
   ```

   ⚠️ **IMPORTANT:** Never commit `appsettings.json` to git! It's already in `.gitignore`.

### 2. Set Up Supabase Database

Run this SQL script in your Supabase SQL Editor (Dashboard → SQL Editor):

```sql
-- Create profiles table for additional user data
create table public.profiles (
  id uuid references auth.users on delete cascade primary key,
  email text,
  full_name text,
  created_at timestamp with time zone default timezone('utc'::text, now()) not null,
  updated_at timestamp with time zone default timezone('utc'::text, now()) not null
);

-- Enable Row Level Security
alter table public.profiles enable row level security;

-- Policy: Users can view their own profile
create policy "Users can view own profile"
  on profiles for select
  using (auth.uid() = id);

-- Policy: Users can update their own profile
create policy "Users can update own profile"
  on profiles for update
  using (auth.uid() = id);

-- Auto-create profile on user signup
create function public.handle_new_user()
returns trigger as $$
begin
  insert into public.profiles (id, email)
  values (new.id, new.email);
  return new;
end;
$$ language plpgsql security definer;

-- Trigger to execute function
create trigger on_auth_user_created
  after insert on auth.users
  for each row execute procedure public.handle_new_user();
```

### 3. Configure Supabase Authentication

1. In your Supabase Dashboard, go to **Authentication** → **Settings**
2. **Disable** "Enable email confirmations" (makes testing easier)
3. Save the changes

### 4. Create a Test User

1. In Supabase Dashboard, go to **Authentication** → **Users**
2. Click **"Add user"**
3. Choose **"Create new user"**
4. Enter:
   - Email: `test@example.com` (or any email)
   - Password: `Test123!` (or any password)
   - **Uncheck** "Auto Confirm User" if you disabled email confirmations
5. Click **"Create user"**

### 5. Build and Run the App

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run on Windows (if on Windows)
dotnet build -t:Run -f net9.0-windows10.0.19041.0

# Run on Android (if you have Android SDK)
dotnet build -t:Run -f net9.0-android
```

### 6. Test the App

1. The app should open to the Welcome page
2. Click **"Continuer"** to go to the login page
3. Enter the test user credentials you created
4. Check **"Se souvenir"** if you want to test the "Remember Me" feature
5. Click **"Se connecter"**
6. You should be redirected to the Home page!

## Troubleshooting

### "SUPABASE_URL not configured" error

- Make sure you created `appsettings.json` from the example file
- Verify the file is in the project root directory
- Check that the JSON syntax is correct
- Rebuild the project: `dotnet clean && dotnet build`

### "Invalid login credentials" error

- Verify the user exists in Supabase Dashboard → Authentication → Users
- Check that you're using the correct email and password
- Make sure email confirmations are disabled in Supabase settings

### App crashes on startup

- Check that your Supabase project is active (not paused)
- Verify the Project URL and anon key are correct in `appsettings.json`
- Check the debug output for detailed error messages

## Sharing with Others

When sharing this project with teammates or testers:

1. **DO NOT** share your `appsettings.json` file
2. **DO** share these instructions
3. Each person needs to:
   - Create their own Supabase project (or get access to yours)
   - Create their own `appsettings.json` with the credentials
   - Run the SQL script to set up the database

Alternatively, you can all share the same Supabase project. In that case:
- Share the Supabase project URL and anon key securely (not via git)
- Everyone uses the same credentials in their local `appsettings.json`
- You can all test with the same users

## Need Help?

If you encounter issues:
1. Check the troubleshooting section above
2. Review the error message in the debug output
3. Verify all setup steps were completed
4. Check that your Supabase project is properly configured
