﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.Data;
using ThriftShop.DataAccess.IRepository;
using ThriftShop.DataAccess.Repository.Services.Generic_Imp;
using ThriftShop.Models;

namespace ThriftShop.DataAccess.Services
{
    public class FeedbackService :Repository<Feedback>,IFeedback
    {
        private readonly ApplicationDbContext _db;
        public FeedbackService(ApplicationDbContext _db):base(_db)
        {
            this._db = _db;
        }

        public  async Task<Feedback> Update(Feedback feedback)
        {
            _db.Feedbacks.Update(feedback);
            return feedback;
        }
    }
}
