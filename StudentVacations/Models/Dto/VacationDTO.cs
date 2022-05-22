// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentVacations.Models
{
    public class VacationDTO
    {
        public long Id { get; set; }
        public int WeekNumberStart { get; set; }
        public int WeekNumberEnd { get; set; }
        public string? Name { get; set; }
        public long? StudentId { get; set; }
    }
}