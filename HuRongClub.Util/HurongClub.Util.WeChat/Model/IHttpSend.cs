﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuRongClub.Util.WeChat.Model
{
    public interface IHttpSend
    {
        string Send(string url, string data);
    }
}