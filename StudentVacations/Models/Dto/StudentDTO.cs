// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentVacations.Models
{
    public class StudentDTO
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public int CoursesCount { get; set; }
        public int VacationsCount { get; set; }
    }
}