global using Polly;
global using Marten;
global using MediatR;
global using Weasel.Core;
global using System.Text;
global using System.Net.Mime;
global using Newtonsoft.Json;
global using Confluent.Kafka;
global using Newtonsoft.Json.Linq;
global using System.Net.Http.Headers;
global using IdentityModel.Client;
global using Microsoft.AspNetCore.Http;
global using System.Threading.RateLimiting;
global using Silos.Core.Reflection;
global using Silos.Core.Domain;
global using Microsoft.Extensions.Logging;
global using Microsoft.OpenApi.Models;
global using Microsoft.Extensions.Options;
global using Microsoft.AspNetCore.Builder;
global using Silos.Core.Testing;
global using Silos.Core.EventBus;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Hosting;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using Microsoft.AspNetCore.Authorization;
global using Silos.Core.Persistence;
global using Microsoft.Extensions.Configuration;
global using Silos.Core.CQRS.CommandHandling;
global using Silos.Core.Infrastructure.Http;
global using Silos.Core.CQRS.QueryHandling;
global using Microsoft.Extensions.Caching.Memory;
global using Silos.Core.Infrastructure.CQRS;
global using Silos.Core.Infrastructure.WebApi;
global using Silos.Core.Infrastructure.EventBus;
global using Microsoft.Extensions.DependencyInjection;
global using Silos.Core.Infrastructure.Workers;
global using Silos.Core.Infrastructure.Identity;
global using Silos.Core.Infrastructure.Integration;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Silos.Core.Infrastructure.Kafka.Consumer;
global using Silos.Core.Infrastructure.Outbox.Workers;
global using Silos.Core.Infrastructure.Outbox.Services;
global using Silos.Core.Infrastructure.Kafka.Producer;
global using Silos.Core.Infrastructure.Kafka.Serialization;
global using Microsoft.Extensions.DependencyInjection.Extensions;