﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Models;

namespace SecretSanta.Service.IServices
{
    public interface IConnectionService
    {
        Connection GetConnectionInGroup(int groupId, string giverId);
        Task<Connection> CreateConnectionsAsync(int groupId, ApplicationUser giver, ApplicationUser receiver);
    }
}