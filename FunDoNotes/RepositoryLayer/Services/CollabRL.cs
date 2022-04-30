using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {
        private readonly FunDoContext funDoContext;

        public CollabRL(FunDoContext funDoContext)
        {
            this.funDoContext = funDoContext;
        }
        public CollabEntity AddCollaborator(Collaborators collaborator, long noteID, long userID)
        {
            try
            {
                var duplicate = funDoContext.CollabTable.Where(x => x.Email == collaborator.Email && x.NoteId == noteID && x.UserId == userID).FirstOrDefault();
                var resNote = funDoContext.NotesTable.Where(x => x.NoteId == noteID && x.UserId == userID).FirstOrDefault();

                if (duplicate == null && resNote != null)
                {
                    CollabEntity collabEntity = new CollabEntity()
                    {
                        Email = collaborator.Email,
                        NoteId = noteID,
                        UserId = userID
                    };
                    funDoContext.CollabTable.Add(collabEntity);
                    int res = funDoContext.SaveChanges();
                    if (res > 0)
                        return collabEntity;
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
        public string RemoveCollaborator(long collabID, long noteID)
        {
            try
            {
                var resCollab = funDoContext.CollabTable.Where(x => x.CollabId == collabID && x.NoteId == noteID).FirstOrDefault();
                if (resCollab != null)
                {
                    funDoContext.CollabTable.Remove(resCollab);
                    int res = funDoContext.SaveChanges();
                    if (res > 0)
                        return "Collaborator removed Successfully";
                    else
                        return "Failed to remove collaborator";
                }
                else
                    return " Collaborator not Found";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CollabEntity> GetAll(long noteID)
        {
            try
            {
                var get = funDoContext.CollabTable.Where(x => x.NoteId == noteID).ToList();
                return get;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
