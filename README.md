# Fitness Tracker Management System
A full-featured fitness tracking web application developed for the Database Systems course at Zewail City. It helps users track workouts, nutrition plans, health metrics, and personal goals, while also supporting roles for trainers and administrators.

# Project Overview
The Fitness Tracker system allows:
- Users to sign up, log in, track workouts, health, nutrition, goals, and send support requests.
- Trainers to manage user-specific workout and nutrition plans.
- Admins to manage user and trainer accounts.
- The project includes pages for user registration/login, dashboards, trainer tools, admin panel, and health data management.

# Features by Role
- User:
View and update personal health metrics.
Track workout plans and fitness goals.
View assigned nutrition plans.
Submit support requests.

- Trainer:
Create and manage workout plans for users.
Design and update nutrition plans.
Supervise user goals.

- Admin:
View and manage all users and trainers.
Add or delete accounts.

# Pages & Functionality

| Page                 | Description                                                       |
| -------------------- | ----------------------------------------------------------------- |
| **Home**             | Landing page with navigation to Sign Up and Login                 |
| **Sign Up**          | Registration with role-based email prefixes (`a-`, `u-`, `t-`)    |
| **Login**            | Redirects user to correct dashboard based on role                 |
| **Admin Panel**      | Manage trainers and users (view, delete, add)                     |
| **Trainer Panel**    | Manage users’ nutrition plans and workouts                        |
| **User Dashboard**   | Access to Health, Workout, Goals, Nutrition, and Support sections |
| **Health Metrics**   | Track weight, height, InBody score                                |
| **Workout Plan**     | Display structured workouts with sets, reps, and target areas     |
| **Goals**            | Set and track fitness goals                                       |
| **Nutrition Plan**   | View meal schedules defined by trainer                            |
| **Support Requests** | Submit requests for help and track request ID                     |

# Database Design
-Entities:
Users, Trainers, Workouts, Nutrition Plans, Goals, Health Metrics, Challenges, Support Requests

- Relationships:
Users follow plans, perform workouts, log metrics, and send support requests.
Trainers assign plans and supervise user progress.
Admins manage both users and trainers.

# Tech Stack
Frontend: HTML/CSS, JavaScript.
Backend: Python / Flask (optional based on implementation).
Database: MySQL or SQLite (based on course requirements).
Tools: ERD diagrams, Schema design, Validation logic.

# How to Run 
This will depend on your implementation. If you have a Flask or HTML project, include:
# Clone the repo
git clone https://github.com/yourusername/fitness-tracker-db.git
cd fitness-tracker-db

# If it's Flask-based
pip install flask
python app.py

# Open browser
http://127.0.0.1:5000/
