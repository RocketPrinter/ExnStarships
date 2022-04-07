using AutoMapper;
using ExnStarships.Data.Entities;
using ExnStarships.Services.Dto;
using ExnStarships.Web.Models;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ExnStarships.Web;

public class MapperProfile : Profile
{
    readonly Regex entityRegex = new Regex(".*", RegexOptions.Compiled);
    readonly Regex dtoRegex = new Regex(".*(?=Dto)", RegexOptions.Compiled);
    readonly Regex viewModelRegex = new Regex(".*(?=ViewModel)", RegexOptions.Compiled);

    // I was reading AutoMapper's docs and profiles seemed neat
    public MapperProfile()
    {
        MapRegex(typeof(Role),entityRegex,typeof(RoleDto),dtoRegex);
        MapRegex(typeof(RoleDto), dtoRegex, typeof(RoleViewModel), viewModelRegex);
    }

    // why spent 30 seconds copy pasting some lines when you can spent 10 minutes writing a fancy function that automates the job?
    void MapRegex(Type markerA, Regex regexA, Type markerB, Regex regexB)
    {
        if (markerA.Namespace == null) throw new ArgumentNullException("markerA has a null namespace");
        if (markerB.Namespace == null) throw new ArgumentNullException("markerB has a null namespace");

        Dictionary<string, Type> freeTypes = new();
        string namespaceA = markerA.Namespace ?? "", namespaceB = markerB.Namespace ?? "";

        foreach (Type type in GetTypesInNamespace(markerA))
        {
            var match = regexA.Match(type.Name);
            if (!match.Success) continue;
            freeTypes.Add(match.Value,type);
        }

        foreach (Type type in GetTypesInNamespace(markerB))
        {
            var match = regexB.Match(type.Name);
            if (match.Success && freeTypes.TryGetValue(match.Value, out var otherType))
            {
                var mapping = CreateMap(otherType,type).ReverseMap();
                freeTypes.Remove(match.Value);
            }
        }

        IEnumerable<Type> GetTypesInNamespace(Type marker) =>
            marker.Assembly.GetTypes()
            .Where(t => t.IsClass && t.Namespace != null && t.Namespace.StartsWith(marker.Namespace!));
    }
}
