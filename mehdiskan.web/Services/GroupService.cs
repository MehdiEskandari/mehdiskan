﻿using mehdiskan.web.Data;
using mehdiskan.web.Helpers;
using mehdiskan.web.Interfaces;
using mehdiskan.web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mehdiskan.web.Services
{
    public class GroupService : IGroupService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GroupService> _logger;

        public GroupService(ApplicationDbContext context, ILogger<GroupService> logger)
        {
            _context = context;
            _logger = logger;
        }



        /// <summary>
        /// Add new group or subgroup to database.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        #region Add new group
        public Group AddGroup(Group group, IFormFile imageFile)
        {
            try
            {
                group.ImageName = imageFile.UploadPhoto();

                _context.Groups.Add(group);
                _context.SaveChanges();

                return group;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }

        public async Task<Group> AddGroupAsync(Group group, IFormFile imageFile)
        {
            try
            {
                group.ImageName = imageFile.UploadPhoto();

                await _context.Groups.AddAsync(group);
                await _context.SaveChangesAsync();

                return group;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }
        #endregion

        /// <summary>
        /// Get all groups and subgroups.
        /// </summary>
        /// <returns></returns>
        #region Get all groups
        public IEnumerable<Group> GetAllGroups() => _context.Groups.ToList();

        public async Task<IEnumerable<Group>> GetAllGroupsAsync() => await _context.Groups.ToListAsync();

        #endregion

        /// <summary>
        /// Get group by id from database.
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        #region Get Group by id
        public Group GetGroupById(int groupId) => _context.Groups.SingleOrDefault(g => g.GroupId == groupId);

        public async Task<Group> GetGroupByIdAsync(int groupId) => await _context.Groups.SingleOrDefaultAsync(g => g.GroupId == groupId);
        #endregion

        /// <summary>
        /// Get all groups.
        /// </summary>
        /// <returns></returns>
        #region Get all groups
        public IEnumerable<Group> GetGroups() => _context.Groups.Where(g => g.ParentId == null).ToList();

        public async Task<IEnumerable<Group>> GetGroupsAsync() => await _context.Groups.Where(g => g.ParentId == null).ToListAsync();
        #endregion

        /// <summary>
        /// Get all subgroups
        /// </summary>
        /// <returns></returns>
        #region Get subgroups
        public IEnumerable<Group> GetSubGroups() => _context.Groups.Where(g => g.ParentId != null).ToList();

        public async Task<IEnumerable<Group>> GetSubGroupsAsync() => await _context.Groups.Where(g => g.ParentId != null).ToListAsync();
        #endregion

        /// <summary>
        /// Remove the group from database.
        /// </summary>
        /// <param name="groupId"></param>
        #region Remove Group
        public void RemoveGroup(int groupId)
        {
            var group = GetGroupById(groupId);

            _context.Groups.Remove(group);
            _context.SaveChanges();
        }

        public async Task RemoveGroupAsync(int groupId)
        {
            var group = GetGroupById(groupId);

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }
        #endregion

        /// <summary>
        /// Update the group's data from database.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        #region Update Group
        public Group UpdateGroup(Group group, IFormFile imageFile)
        {
            try
            {
                group.ImageName = imageFile.UpdateUploadedGroupPhoto(group);

                _context.Groups.Update(group);
                _context.SaveChanges();

                return group;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }

        public async Task<Group> UpdateGroupAsync(Group group, IFormFile imageFile)
        {
            try
            {
                group.ImageName = imageFile.UpdateUploadedGroupPhoto(group);

                _context.Groups.Update(group);
                await _context.SaveChangesAsync();

                return group;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");
                return null;
            }
        }
        #endregion


    }
}
