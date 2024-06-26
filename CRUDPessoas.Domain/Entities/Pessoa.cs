﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDPessoas.Domain.Entities
{
    public class Pessoa
    {
        public int Id { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public DateTime ModifiedAt { get; private set; } = DateTime.Now;

        public void ReceiveId(int id)
        {
            Id = id;
        }

        public void UpdateModifiedAt()
        {
            ModifiedAt = DateTime.Now;
        }
    }
}
