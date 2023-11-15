# Capstone Epicode Project

Welcome to the Capstone Epicode repository! This project, developed as the final end-of-course project, utilizes ASP.NET Core and Entity Framework to manage an online auction application with a primary focus on showcasing and selling artwork. The project is enhanced with Sass, allowing for extensive customization throughout the entire site.

![Immagine di Esempio 1](Content/imgDefault/LogoStone.png)

## Key Features

- **Auctions:** Users can participate in online auctions, place bids, and win unique pieces of artwork.
- **User Management:** Registration, login, and user profiles.
- **Artwork Management:** Adding, editing, and viewing artwork up for auction.
- **Commenting System:** Users can leave comments on artwork and engage in discussions. The system supports re-commenting on other users' comments.
- **Admin Dashboard:** Advanced features for administrators, including product and user management.

## Prerequisites

Make sure you have the following installed:

- [Visual Studio](https://visualstudio.microsoft.com/)
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server/)

## Project Setup

1. Clone the repository: `git clone https://github.com/AlessioDiPretoro/CapsoneEpicode.git`
2. Navigate to the project directory: `cd CapsoneEpicode`
3. Open the project in Visual Studio.
4. Configure the connection string in the `appsettings.json` file.

## Running the Application

1. Ensure the startup project is set to `CapstoneEpicode.Web`.
2. Press F5 or start the application from Visual Studio.

The application should be accessible at [https://localhost:"YourPortNumber"](https://localhost:"YourPortNumber").

## Controller Functions

### `AuctionsController`

- **Index:** Display a list of ongoing auctions.
- **Details:** Display details of a specific auction.
- **Create:** Create a new auction.
- **Edit:** Modify an existing auction.
- **Delete:** Delete an auction.

### `UserController`

- **Register:** Register a new user.
- **Login:** Log in an existing user.
- **Logout:** Log out the current user.
- **Profile:** Display and edit user profile information.

### `ArtworkController`

- **Index:** Display a list of artwork available for auction.
- **Details:** Display details of a specific artwork.
- **Create:** Add a new artwork to the auction.
- **Edit:** Modify an existing artwork.
- **Delete:** Remove an artwork from the auction.

### `CommentController`

- **AddComment:** Add a new comment to an artwork.
- **EditComment:** Edit an existing comment.
- **DeleteComment:** Delete a comment.

### `AdminController`

- **Dashboard:** Display an admin dashboard with advanced management features.
- **ManageUsers:** View and manage registered users.
- **ManageArtwork:** View and manage artwork listings.

## Contributing

We are happy to accept contributions! Feel free to open a new issue or submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.