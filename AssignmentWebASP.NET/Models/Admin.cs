﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssignmentWebASP.NET.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Admin
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email của bạn.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Làm ơn nhập đúng email của bạn.")]
        public string email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu của bạn.")]
        [DataType(DataType.Password)]
        [StringLength(35, MinimumLength = 4)]
        public string password { get; set; }


        public string firstName { get; set; }
        public string lastName { get; set; }
        public int phone { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public System.DateTime createAt { get; set; }

        public string LoginErrorMessage { get; set; }
    }
}