# U-Learning: Personalized Educational Platform

## Project Description

U-Learning is a dynamic, user-friendly educational platform designed to deliver personalized learning experiences. By integrating interactive courses, AI-driven recommendations, and collaborative tools, it empowers learners of all ages to engage with education in a meaningful way. Built with ASP.NET MVC and Entity Framework, U-Learning ensures a scalable and robust foundation for modern e-learning.

## Features

- **Personalized Learning**: AI-driven course and content recommendations tailored to individual learner needs.
- **Interactive Courses**: Engaging course materials with multimedia and interactive elements.
- **Collaborative Tools**: Features for group discussions, peer reviews, and instructor-student interaction.
- **User Management**: Role-based access for learners, instructors, and administrators.
- **Scalable Database**: Efficient data management for users, courses, and materials using Entity Framework.

## Tech Stack

- **Backend**: ASP.NET MVC (.NET Framework/Core)
- **ORM**: Entity Framework (Code-First approach)
- **Database**: SQL Server
- **Frontend**: HTML, CSS, Bootstrap, JavaScript
- **Tools**: Visual Studio, SQL Server Management Studio, Git

## Prerequisites

- .NET SDK (version 6.0 or later)
- SQL Server (LocalDB or Express)
- Visual Studio 2022 (or later)
- Git

## Installation

1. **Clone the Repository**:

   ```bash
   git clone https://github.com/yourusername/u-learning.git
   cd u-learning
   ```

2. **Restore Dependencies**:

   - Open the solution (`U-Learning.sln`) in Visual Studio.
   - Restore NuGet packages:

     ```bash
     dotnet restore
     ```

3. **Configure the Database**:

   - Update the connection string in `appsettings.json` or `web.config`:

     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ULearningDB;Trusted_Connection=True;"
     }
     ```
   - Run migrations to set up the database:

     ```bash
     dotnet ef database update
     ```

4. **Run the Application**:

   - Build and run the project in Visual Studio (F5) or use the CLI:

     ```bash
     dotnet run
     ```
   - Access the app at `https://localhost:5001` (port may vary).

## Database Schema

The database includes the following key tables:

- **Users**: Stores learner, instructor, and admin details (e.g., ID, name, role).
- **Courses**: Manages course data, including interactive content and metadata.
- **Materials**: Stores multimedia and interactive learning resources.
- **Recommendations**: Tracks AI-generated suggestions for personalized learning paths.
- **Collaborations**: Supports discussion posts, group activities, and peer reviews.

## Usage

- **Learners**: Explore recommended courses, engage with interactive content, and collaborate with peers.
- **Instructors**: Create and manage courses, upload materials, and facilitate discussions.
- **Admins**: Oversee user management, course approvals, and platform settings.
- Sample credentials (for testing):
  - Admin: `admin@u-learning.com` / Password: `Admin@123`
  - Learner: `learner@u-learning.com` / Password: `Learner@123`

## Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature-name`).
3. Commit changes (`git commit -m 'Add feature'`).
4. Push to the branch (`git push origin feature-name`).
5. Open a pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Contact

For questions or feedback, reach out via GitHub Issues.