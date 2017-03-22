using System.Runtime.Serialization;

namespace NorthwindModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [DataContract]
    [KnownType(typeof(Employee))]
    [KnownType(typeof(Order))]
    [KnownType(typeof(Territory))]
    [KnownType(typeof(ICollection<Territory>))]
    [KnownType(typeof(ICollection<Employee>))]
    [KnownType(typeof(ICollection<Order>))]
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Employees1 = new HashSet<Employee>();
            Orders = new HashSet<Order>();
            Territories = new HashSet<Territory>();
        }

        [DataMember]
        public int EmployeeID { get; set; }

        [DataMember]
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [DataMember]
        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        [DataMember]
        [StringLength(30)]
        public string Title { get; set; }

        [DataMember]
        [StringLength(25)]
        public string TitleOfCourtesy { get; set; }

        [DataMember]
        public DateTime? BirthDate { get; set; }

        [DataMember]
        public DateTime? HireDate { get; set; }

        [DataMember]
        [StringLength(60)]
        public string Address { get; set; }

        [DataMember]
        [StringLength(15)]
        public string City { get; set; }

        [DataMember]
        [StringLength(15)]
        public string Region { get; set; }

        [DataMember]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [DataMember]
        [StringLength(15)]
        public string Country { get; set; }

        [DataMember]
        [StringLength(24)]
        public string HomePhone { get; set; }

        [DataMember]
        [StringLength(4)]
        public string Extension { get; set; }

        [DataMember]
        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        [DataMember]
        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [DataMember]
        public int? ReportsTo { get; set; }

        [DataMember]
        [StringLength(255)]
        public string PhotoPath { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees1 { get; set; }

        [DataMember]
        public virtual Employee Employee1 { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Territory> Territories { get; set; }

        public override string ToString()
        {
            return string.Format("EmployeeID: {0}, LastName: {1}, FirstName: {2}, Title: {3}, TitleOfCourtesy: {4}, BirthDate: {5}, HireDate: {6}, Address: {7}, City: {8}, Region: {9}, PostalCode: {10}, Country: {11}, HomePhone: {12}, Extension: {13}, Photo: {14}, Notes: {15}, ReportsTo: {16}, PhotoPath: {17}, Employees1: {18}, Employee1: {19}, Orders: {20}, Territories: {21}", EmployeeID, LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Photo, Notes, ReportsTo, PhotoPath, Employees1, Employee1, Orders, Territories);
        }
    }
}
