using AutoMapper;
using BLL.Dto;
using DAL.Domain;
using DAL.Extension.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Mapping
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Student, StudentDto>();
            CreateMap<Student, StudentListDto>();

            //CreateMap<PaginatedEnumerable<Student>, PaginatedEnumerableDto<StudentListDto>>()
            //        //.ForMember(p => p.Items, opt => opt.MapFrom(s => s.Items))
            //        ;

            CreateMap<PaginatedEnumerable, PaginatedEnumerableDto>();
            CreateMap<PaginatedEnumerable<Student>, PaginatedEnumerableDto<StudentDto>>();
            //       //.ForMember(p => p.Items, opt => opt.MapFrom(s => s.Items))
            //       ;

        }
    }
}
