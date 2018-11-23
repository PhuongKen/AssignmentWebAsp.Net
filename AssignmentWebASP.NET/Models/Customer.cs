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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.Images = new HashSet<Image>();
            this.Orders = new HashSet<Order>();
        }

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


        [DataType(DataType.Password)]
        [Compare("password")]
        [DisplayName("Xác nhận mật khẩu.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên của bạn.")]
        [StringLength(35, MinimumLength = 4)]
        public string name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại của bạn.")]
        //[MaxLength(10)]
        public int phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày sinh của bạn.")]
        [DataType(DataType.Date)]
        public System.DateTime dateOfBirth { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giới tính của bạn.")]
        public int gender { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ của bạn.")]
        [StringLength(100, MinimumLength = 4)]
        public string address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập ngày tạo tài khoản của bạn.")]
        [DataType(DataType.Date)]
        public System.DateTime createAt { get; set; }

        public string LoginErrorMessage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}