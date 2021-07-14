using MediatR;
using Myriolang.ConlangDev.API.Models;

namespace Myriolang.ConlangDev.API.Commands.Profiles
{
    public class FetchProfileQuery : IRequest<Profile>
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }
}