using Taskify.ApiService.Data.Entities;

namespace Taskify.ApiService.Data;

public static class DbContextSeed
{
    public static async Task SeedAsync(TaskifyDbContext context)
    {
        // Check if database is already seeded
        if (context.Users.Any())
        {
            return;
        }

        // Seed Users (1 PM + 4 Engineers)
        var users = new List<User>
        {
            new User
            {
                Name = "Alice Johnson",
                Email = "alice@taskify.com",
                Role = "ProductManager",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Name = "Bob Smith",
                Email = "bob@taskify.com",
                Role = "Engineer",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Name = "Charlie Brown",
                Email = "charlie@taskify.com",
                Role = "Engineer",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Name = "Diana Prince",
                Email = "diana@taskify.com",
                Role = "Engineer",
                CreatedAt = DateTime.UtcNow
            },
            new User
            {
                Name = "Ethan Hunt",
                Email = "ethan@taskify.com",
                Role = "Engineer",
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Users.AddRange(users);
        await context.SaveChangesAsync();

        // Seed Projects
        var projects = new List<Project>
        {
            new Project
            {
                Name = "E-commerce Platform",
                Description = "Building a modern e-commerce platform with React and .NET",
                CreatedAt = DateTime.UtcNow
            },
            new Project
            {
                Name = "Mobile App Redesign",
                Description = "Redesigning the company's mobile app with improved UX",
                CreatedAt = DateTime.UtcNow
            },
            new Project
            {
                Name = "Data Analytics Dashboard",
                Description = "Creating a comprehensive analytics dashboard for business insights",
                CreatedAt = DateTime.UtcNow
            }
        };

        context.Projects.AddRange(projects);
        await context.SaveChangesAsync();

        // Seed Tasks for Project 1 (E-commerce Platform) - 15 tasks
        var project1Tasks = new List<TaskItem>
        {
            new TaskItem { Title = "Setup project repository", Description = "Initialize Git repository and project structure", Status = "Done", AssignedToId = users[1].Id, ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Design database schema", Description = "Create ERD and design database tables", Status = "Done", AssignedToId = users[2].Id, ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Implement user authentication", Description = "Add JWT-based authentication", Status = "In Progress", AssignedToId = users[1].Id, ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Create product catalog API", Description = "Build REST API for product management", Status = "In Progress", AssignedToId = users[3].Id, ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Implement shopping cart", Description = "Build shopping cart functionality", Status = "To Do", AssignedToId = users[2].Id, ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Setup payment gateway", Description = "Integrate Stripe payment processing", Status = "To Do", AssignedToId = users[4].Id, ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Design product listing page", Description = "Create responsive product grid layout", Status = "In Review", AssignedToId = users[1].Id, ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Implement search functionality", Description = "Add full-text search for products", Status = "To Do", ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Create order management", Description = "Build order tracking and management", Status = "To Do", AssignedToId = users[3].Id, ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Setup email notifications", Description = "Configure SendGrid for order emails", Status = "To Do", ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Implement product reviews", Description = "Add review and rating system", Status = "To Do", AssignedToId = users[2].Id, ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Create admin dashboard", Description = "Build admin panel for management", Status = "In Progress", AssignedToId = users[4].Id, ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Setup CI/CD pipeline", Description = "Configure GitHub Actions for deployment", Status = "In Review", AssignedToId = users[1].Id, ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Write API documentation", Description = "Document all API endpoints with OpenAPI", Status = "To Do", ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] },
            new TaskItem { Title = "Perform security audit", Description = "Review and fix security vulnerabilities", Status = "To Do", AssignedToId = users[3].Id, ProjectId = projects[0].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[0] }
        };

        // Seed Tasks for Project 2 (Mobile App Redesign) - 15 tasks
        var project2Tasks = new List<TaskItem>
        {
            new TaskItem { Title = "Conduct user research", Description = "Interview users and gather feedback", Status = "Done", AssignedToId = users[0].Id, ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Create wireframes", Description = "Design low-fidelity wireframes", Status = "Done", AssignedToId = users[1].Id, ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Design UI mockups", Description = "Create high-fidelity designs in Figma", Status = "In Progress", AssignedToId = users[1].Id, ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Setup React Native project", Description = "Initialize new React Native app", Status = "Done", AssignedToId = users[2].Id, ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Implement navigation", Description = "Setup React Navigation with tabs", Status = "In Progress", AssignedToId = users[3].Id, ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Create component library", Description = "Build reusable UI components", Status = "In Progress", AssignedToId = users[4].Id, ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Implement dark mode", Description = "Add dark mode theme support", Status = "To Do", AssignedToId = users[2].Id, ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Add animations", Description = "Implement smooth transitions and animations", Status = "To Do", ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Setup state management", Description = "Configure Redux Toolkit", Status = "In Review", AssignedToId = users[3].Id, ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Implement offline mode", Description = "Add offline data persistence", Status = "To Do", AssignedToId = users[4].Id, ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Add push notifications", Description = "Setup Firebase Cloud Messaging", Status = "To Do", ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Optimize performance", Description = "Reduce bundle size and improve loading", Status = "To Do", AssignedToId = users[2].Id, ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Write E2E tests", Description = "Create Detox tests for critical flows", Status = "To Do", AssignedToId = users[3].Id, ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Setup app distribution", Description = "Configure TestFlight and Play Console", Status = "To Do", ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] },
            new TaskItem { Title = "Conduct beta testing", Description = "Gather feedback from beta users", Status = "To Do", AssignedToId = users[0].Id, ProjectId = projects[1].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[1] }
        };

        // Seed Tasks for Project 3 (Data Analytics Dashboard) - 15 tasks
        var project3Tasks = new List<TaskItem>
        {
            new TaskItem { Title = "Define analytics requirements", Description = "Document required metrics and KPIs", Status = "Done", AssignedToId = users[0].Id, ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Setup data warehouse", Description = "Configure PostgreSQL data warehouse", Status = "Done", AssignedToId = users[2].Id, ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Create ETL pipelines", Description = "Build data extraction and transformation", Status = "In Progress", AssignedToId = users[4].Id, ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Design dashboard layout", Description = "Create responsive dashboard design", Status = "Done", AssignedToId = users[1].Id, ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Implement chart library", Description = "Integrate Chart.js for visualizations", Status = "In Progress", AssignedToId = users[3].Id, ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Create KPI widgets", Description = "Build reusable KPI card components", Status = "In Progress", AssignedToId = users[1].Id, ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Add data filters", Description = "Implement date range and category filters", Status = "To Do", AssignedToId = users[3].Id, ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Setup real-time updates", Description = "Implement SignalR for live data", Status = "To Do", ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Create export functionality", Description = "Add CSV and PDF export options", Status = "To Do", AssignedToId = users[2].Id, ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Implement drill-down", Description = "Add detailed views for metrics", Status = "In Review", AssignedToId = users[4].Id, ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Add user permissions", Description = "Implement role-based access control", Status = "To Do", ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Optimize query performance", Description = "Add indexes and optimize SQL queries", Status = "To Do", AssignedToId = users[2].Id, ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Setup caching layer", Description = "Implement Redis for data caching", Status = "To Do", AssignedToId = users[4].Id, ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Create custom reports", Description = "Build report builder interface", Status = "To Do", ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] },
            new TaskItem { Title = "Add email scheduling", Description = "Setup automated report emails", Status = "To Do", AssignedToId = users[3].Id, ProjectId = projects[2].Id, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, Project = projects[2] }
        };

        context.Tasks.AddRange(project1Tasks);
        context.Tasks.AddRange(project2Tasks);
        context.Tasks.AddRange(project3Tasks);
        await context.SaveChangesAsync();
    }
}
