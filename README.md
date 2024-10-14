
# Social Media Platform

A full-stack social media platform built using **ASP.NET Core MVC**. This platform provides users with the ability to create profiles, post media (images, videos, GIFs), manage friends, and interact with posts through comments, likes, and reactions. The platform also includes account privacy and security settings, notifications, and content preferences.

## Table of Contents
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Installation](#installation)
- [Usage](#usage)
- [Media Management](#media-management)
- [Project Structure](#project-structure)

---

## Features

- **User Profiles**: Manage your personal profile with a bio, profile picture, and location.
- **Media Uploads**: Upload and optimize multiple images, videos, and GIFs in posts.
- **Timeline & News Feed**: View posts from friends, comment, like, and react to them.
- **Friends Management**: Add, remove, and view friends on the platform.
- **Privacy Settings**: Control who can see your profile and posts.
- **Security Settings**: Change your password, enable two-factor authentication, and view recent login activity.
- **Notifications**: Manage email and push notifications, customize sound and vibration settings.
- **Responsive Design**: Fully responsive and mobile-friendly interface.

---

## Tech Stack

- **Backend**: ASP.NET Core MVC
- **Frontend**: HTML, CSS (Grid and Flexbox), JavaScript
- **Database**: SQL Server (Entity Framework Core)
- **Authentication**: ASP.NET Identity with Two-Factor Authentication (2FA)
- **Media Processing**: Image and Video resizing, compression, and optimization
- **Version Control**: Git & GitHub

---

## Installation

To run the project locally, follow these steps:

1. Clone the repository:
   \`\`\`bash
   git clone https://github.com/your-username/social-media-platform.git
   cd social-media-platform
   \`\`\`

2. Restore dependencies:
   \`\`\`bash
   dotnet restore
   \`\`\`

3. Build the project:
   \`\`\`bash
   dotnet build
   \`\`\`

4. Apply database migrations:
   \`\`\`bash
   dotnet ef database update
   \`\`\`

5. Run the application:
   \`\`\`bash
   dotnet run
   \`\`\`

6. Open your browser and navigate to:
   \`\`\`
   http://localhost:5000
   \`\`\`

---

## Usage

1. **Sign Up / Login**: Create a new account or log in with existing credentials.
2. **Create Posts**: Upload images, videos, or GIFs in posts and share them with friends.
3. **Interact with Posts**: Like, react, comment, and share posts from others.
4. **Manage Your Profile**: Update your profile information, change your profile picture, and control privacy settings.
5. **Notifications**: Customize email and push notifications based on interactions.
6. **Content Filtering**: Set preferences for content you wish to see in your news feed.

---

## Media Management

The platform supports uploading multiple media types (images, videos, and GIFs) in posts, with built-in support for:

- Automatic resizing of large images for optimized display.
- Video and GIF compression to save bandwidth.
- Maintaining high-quality media while ensuring fast load times.


## Project Structure

```
|-- Controllers
    |-- AccountController.cs     # Handles login, registration, and security
    |-- ProfileController.cs     # Handles user profile display and editing
    |-- PostsController.cs       # Manages posts, comments, reactions, and media uploads
    |-- FriendsController.cs     # Handles friend requests and friend management
|-- Models
    |-- User.cs                  # User model with profile details, settings
    |-- Post.cs                  # Post model with support for media (images, videos)
    |-- Comment.cs               # Comment model
|-- Views
    |-- Account/
    |-- Profile/
    |-- Posts/
|-- wwwroot
    |-- css/                     # Custom styles
    |-- js/                      # Custom scripts
    |-- media/                   # Uploaded media files
```


