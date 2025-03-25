using MyMvcApp.Controllers;
using MyMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MyMvcApp.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public void Index_ReturnsViewWithAllUsers()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Clear();
            UserController.userlist.Add(new User { Id = 1, Name = "Test", Email = "test@example.com" });

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
        }

        [Fact]
        public void Details_ExistingUser_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Clear();
            UserController.userlist.Add(new User { Id = 2, Name = "User2", Email = "user2@example.com" });

            // Act
            var result = controller.Details(2) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as User;
            Assert.Equal(2, model.Id);
        }

        [Fact]
        public void Details_NonExistingUser_ReturnsNotFoundResult()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Clear();

            // Act
            var result = controller.Details(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_Get_ReturnsViewResult()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_Post_AddsUser_AndRedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Clear();
            var user = new User { Id = 3, Name = "User3", Email = "user3@example.com" };

            // Act
            var result = controller.Create(user) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Single(UserController.userlist);
        }

        [Fact]
        public void Edit_Get_ExistingUser_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Clear();
            UserController.userlist.Add(new User { Id = 4, Name = "User4", Email = "user4@example.com" });

            // Act
            var result = controller.Edit(4) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as User;
            Assert.Equal(4, model.Id);
        }

        [Fact]
        public void Edit_Get_NonExistingUser_ReturnsNotFoundResult()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Clear();

            // Act
            var result = controller.Edit(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_UpdatesUser_AndRedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Clear();
            UserController.userlist.Add(new User { Id = 5, Name = "OldName", Email = "old@example.com" });
            var updatedUser = new User { Name = "NewName", Email = "new@example.com" };

            // Act
            var result = controller.Edit(5, updatedUser) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("NewName", UserController.userlist[0].Name);
        }

        [Fact]
        public void Delete_Get_ExistingUser_ReturnsViewWithUser()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Clear();
            UserController.userlist.Add(new User { Id = 6, Name = "User6", Email = "user6@example.com" });

            // Act
            var result = controller.Delete(6) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as User;
            Assert.Equal(6, model.Id);
        }

        [Fact]
        public void Delete_Get_NonExistingUser_ReturnsNotFoundResult()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Clear();

            // Act
            var result = controller.Delete(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Post_RemovesUser_AndRedirectsToIndex()
        {
            // Arrange
            var controller = new UserController();
            UserController.userlist.Clear();
            UserController.userlist.Add(new User { Id = 7, Name = "User7", Email = "user7@example.com" });
            var formCollection = new Microsoft.AspNetCore.Http.FormCollection(new Dictionary<string, Microsoft.Extensions.Primitives.StringValues>());

            // Act
            var result = controller.Delete(7, formCollection) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
            Assert.Empty(UserController.userlist);
        }
    }
}