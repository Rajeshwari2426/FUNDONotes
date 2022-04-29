using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        private readonly FunDoContext funDoContext;
        private readonly IConfiguration configuration;
        public NotesRL(FunDoContext funDoContext, IConfiguration configuration)
        {
            this.funDoContext = funDoContext;
            this.configuration = configuration;
        }
        public NotesEntity CreateNote(Notes createNotes, long userID)
        {
            try
            {
                NotesEntity notesEntity = new NotesEntity();
                notesEntity.Title = createNotes.Title;
                notesEntity.Description = createNotes.Description;
                notesEntity.Color = createNotes.Color;
                notesEntity.Image = createNotes.Image;
                notesEntity.Reminder = createNotes.Reminder;
                notesEntity.IsArchive = createNotes.IsArchive;
                notesEntity.IsPin = createNotes.IsPin;
                notesEntity.IsTrash = createNotes.IsTrash;
                notesEntity.CreatedAt = DateTime.Now;
                notesEntity.ModifiedAt = DateTime.Now;
                notesEntity.UserId = userID;
                funDoContext.NotesTable.Add(notesEntity);
                int result = funDoContext.SaveChanges();
                if (result > 0)
                {
                    return notesEntity;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NotesEntity> RetriveNotes(long userID)
        {
            try
            {
                var getNotes = funDoContext.NotesTable.Where(x => x.UserId == userID).ToList();
                return getNotes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NotesEntity UpdateNote(Notes updateNotes, long noteID, long userID)
        {
            try
            {
                var result = funDoContext.NotesTable.Where(n => n.NoteId == noteID && n.UserId == userID).FirstOrDefault();
                if (result != null)
                {
                    result.Title = string.IsNullOrEmpty(updateNotes.Title) ? result.Title : updateNotes.Title;
                    result.Description = string.IsNullOrEmpty(updateNotes.Description) ? result.Description : updateNotes.Description;
                    result.Reminder = updateNotes.Reminder.CompareTo(result.Reminder) == 0 ? result.Reminder : updateNotes.Reminder;
                    result.Color = string.IsNullOrEmpty(updateNotes.Color) ? result.Color : updateNotes.Color;
                    result.Image = string.IsNullOrEmpty(updateNotes.Image) ? result.Image : updateNotes.Image;
                    result.IsTrash = updateNotes.IsTrash.CompareTo(result.IsTrash) == 0 ? result.IsTrash : updateNotes.IsTrash;
                    result.IsArchive = updateNotes.IsArchive.CompareTo(result.IsArchive) == 0 ? result.IsArchive : updateNotes.IsArchive;
                    result.IsPin = updateNotes.IsPin.CompareTo(result.IsPin) == 0 ? result.IsPin : updateNotes.IsPin;
                    result.ModifiedAt = DateTime.Now;
                    funDoContext.NotesTable.Update(result);
                    funDoContext.SaveChanges();
                    return result;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteNote(long noteID, long userID)
        {
            try
            {
                var result = funDoContext.NotesTable.Where(n => n.NoteId == noteID && n.UserId == userID).FirstOrDefault();
                if (result != null)
                {
                    funDoContext.NotesTable.Remove(result);
                    funDoContext.SaveChanges();
                    return "Note Deleted Successfully";
                }
                else
                    return "Failed To Delete The Note";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NotesEntity IsArchieveOrNot(long noteID, long userID)
        {
            try
            {
                var result = funDoContext.NotesTable.Where(n => n.NoteId == noteID && n.UserId == userID).FirstOrDefault();
                if (result != null)
                {
                    if (result.IsArchive == false)
                        result.IsArchive = true;
                    else
                        result.IsArchive = false;
                    funDoContext.SaveChanges();
                    return result;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NotesEntity IsPinnedOrNot(long noteID, long userID)
        {
            try
            {
                var result = funDoContext.NotesTable.Where(n => n.NoteId == noteID && n.UserId == userID).FirstOrDefault();
                if (result != null)
                {
                    if (result.IsPin == false)
                        result.IsPin = true;
                    else
                        result.IsPin = false;
                    funDoContext.SaveChanges();
                    return result;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NotesEntity IsTrashOrNot(long noteID, long userID)
        {
            try
            {
                var result = funDoContext.NotesTable.Where(n => n.NoteId == noteID && n.UserId == userID).FirstOrDefault();
                if (result != null)
                {
                    if (result.IsTrash == false)
                        result.IsTrash = true;
                    else
                        result.IsTrash = false;
                    funDoContext.SaveChanges();
                    return result;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public NotesEntity UploadImage(long noteID, long userID, IFormFile imagePath)
        {
            try
            {
                var result = funDoContext.NotesTable.Where(x => x.NoteId == noteID && x.UserId == userID).FirstOrDefault();
                if (result != null)
                {
                    Account account = new Account(
                   "dlbzk1wve",
                    "388787964318482",
                    "BciHVKGa291hoX4e56TpiUYIkfY");
                    Cloudinary cloudinary = new Cloudinary(account);
                    cloudinary.Api.Secure = true;
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(imagePath.FileName, imagePath.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    if (uploadResult != null)
                    {
                        result.Image = uploadResult.Url.ToString();
                        result.ModifiedAt = DateTime.Now;
                        funDoContext.NotesTable.Update(result);
                        funDoContext.SaveChanges();
                        return result;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
