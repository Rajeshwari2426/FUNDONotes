using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public LabelEntity AddLabel(Label label, long userId)
        {
            try
            {
                return labelRL.AddLabel(label, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LabelEntity EditLabel(string newName, long labelId, long userId)
        {
            try
            {
                return labelRL.EditLabel(newName, labelId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LabelEntity> GetAll()
        {
            try
            {
                return labelRL.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string RemoveLabel(long labelId, long noteId, long userId)
        {
            try
            {
                return labelRL.RemoveLabel(labelId, noteId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
