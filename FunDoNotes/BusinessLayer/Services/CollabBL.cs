using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL collabRL;
        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public CollabEntity AddCollaborator(Collaborators collaborator, long noteID, long userID)
        {
            try
            {
                return collabRL.AddCollaborator(collaborator, noteID, userID);
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
                return collabRL.RemoveCollaborator(collabID, noteID);
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
                return collabRL.GetAll(noteID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CollabEntity> GetAllNotes()
        {
            try
            {
                return collabRL.GetAllNotes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
