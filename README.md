# Capstone Epicode Project

Welcome to LePieCreazioni!!! 
My gallery and e-commerce project for show and sell your artworks!!!

It's simple to manage the admin side and the client side it's nice and easy.
It's all responsive.

This project si build with ASP.NET Entity Framework and is enhanced with Sass, allowing for extensive customization throughout the entire site.


## Key Features

- **Auctions:** Users can participate in online auctions, place bids, and win unique pieces of artwork.
- **User Management:** Registration, login, and user profiles.
- **Artwork Management:** Adding, editing, and viewing artwork up for auction.
- **Commenting System:** Users can leave comments on artwork and engage in discussions. The system supports re-commenting on other users' comments.
- **Admin Dashboard:** Advanced features for administrators, including product and user management.
- **Auction Scheduler:** Automatic scheduling of auctions using the `_AuctionScheduler` component.
- **Close Auction Job:** Automated closing of auctions and determining winners using the `_CloseAuctionJob`.
- **Mailer Integration:** Email notifications and communication facilitated by the `_Mailer` component.

## Prerequisites

Make sure you have the following installed:

- [Visual Studio](https://visualstudio.microsoft.com/)
- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server/)

## Project Setup

1. Clone the repository: `git clone https://github.com/AlessioDiPretoro/CapsoneEpicode.git`
2. Navigate to the project directory: `cd CapsoneEpicode`
3. Open the project in Visual Studio.
4. Configure the connection string in the `Web.config` file 
5. Create your firt migration and update database for create the database.
6. Configure you email provider info in the `_Mailer.cs` file.
7. On the database manual create the user with roles Admin and SuperAdmin.
8. Enyoi it. 


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

## Scheduler and Jobs

### `_AuctionScheduler`

- **ScheduleAuction:** Automatically schedules upcoming auctions.

### `_CloseAuctionJob`

- **CloseAuction:** Automated job to close auctions and determine winners.

## Mailer

### `_Mailer`

- **SendEmail:** Handles the sending of emails for notifications and communication.

## Contributing

I'm happy to accept contributions! Feel free to open a new issue or submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.