﻿using System;

namespace Aafp.MyCme.Web.ViewModels
{
    public class JsonResultViewModel<T>
    {
        public T Data { get; set; }

        public bool HasError { get; set; }

        public string ErrorMessage { get; set; }

        public Guid? SessionKey { get; set; }
    }
}