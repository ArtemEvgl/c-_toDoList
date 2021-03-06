﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.BL.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.BL.Model;
using System.Runtime.Remoting.Messaging;

namespace ToDoList.BL.Controller.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        

        [TestMethod()]
        public void SetNewUserDataTest()
        {
            //Arrange
            DateTime birthDay = DateTime.Parse("01.01.1995");            
            string userName = Guid.NewGuid().ToString();
            //Act
            var controller = new UserController(userName);
            controller.SetNewUserData(birthDay);
            //Assert
            Assert.AreEqual(birthDay, controller.CurrentUser.BirthDay);
        
        }

        [TestMethod()]
        public void SetNewTaskTest()
        {
            string descrTask = Guid.NewGuid().ToString();
            DateTime date = DateTime.Now.AddDays(10);
            string userName = Guid.NewGuid().ToString();
            var controller = new UserController(userName);
            controller.SetNewUserData(DateTime.Parse("01.01.1995"));
            controller.infoMsges += (msg) => { return; };
            int sizeListTasks = controller.CurrentUser.Tasks.Count;
            Model.Task task = new Model.Task(descrTask, ++sizeListTasks, date);

            controller.SetNewTask(date, descrTask);
            

            Assert.AreEqual(descrTask, controller.CurrentUser.Tasks.Find(i=> i.Equals(task)).Description);

        }

        [TestMethod()]
        public void SetAcceptTaskTest()
        {
            string descrTask = Guid.NewGuid().ToString();
            string userName = Guid.NewGuid().ToString();
            DateTime date = DateTime.Now.AddDays(10);
            var controller = new UserController(userName);
            controller.SetNewUserData(DateTime.Parse("01.01.1995"));
            controller.infoMsges += (msg) => { return; };

            controller.SetNewTask(date, descrTask);
            Model.Task task = controller.CurrentUser.Tasks.First(i => i.Description == descrTask);

            controller.SetAcceptTask(task.Id);

            Assert.AreEqual(true, task.Accept);
        }

        [TestMethod()]
        public void EditTaskTest()
        {
            string descrTask = Guid.NewGuid().ToString();
            string newDescrTask = Guid.NewGuid().ToString();
            string userName = Guid.NewGuid().ToString();
            DateTime date = DateTime.Now.AddDays(10);
            var controller = new UserController(userName);
            controller.SetNewUserData(DateTime.Parse("01.01.1995"));
            controller.infoMsges += (msg) => { return; };
            controller.SetNewTask(date, descrTask);
            Model.Task task = controller.CurrentUser.Tasks.First(i => i.Description == descrTask);

            controller.EditTask(task.Id, date, newDescrTask);

            Assert.AreEqual(newDescrTask, task.Description);
        }

        //[TestMethod()]
        //public void GetStatsTest()
        //{
        //    Assert.Fail();
        //}
    }
}