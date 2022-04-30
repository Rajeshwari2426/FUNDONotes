﻿using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        public LabelEntity AddLabel(Label label, long userId);
        public LabelEntity EditLabel(string newName, long labelId, long userId);
        public string RemoveLabel(long labelId, long noteId, long userId);
        public List<LabelEntity> GetAll();
    }
}
