global using Marten;
global using System.Net;
global using Marten.Events;
global using Newtonsoft.Json;
global using Marten.Events.Projections;
global using Marten.Events.Aggregation;
global using Silos.Core.Domain;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Options;
global using Silos.Core.Exceptions;
global using Silos.Customers.Domain;
global using Silos.Core.Persistence;
global using Silos.Core.Infrastructure;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Authentication;
global using Silos.Core.CQRS.QueryHandling;
global using Silos.Customers.Domain.Events;
global using System.ComponentModel.DataAnnotations;
global using Silos.Core.Infrastructure.Http;
global using Silos.Core.Infrastructure.Marten;
global using Silos.Customers.Domain.Commands;
global using Silos.Core.CQRS.CommandHandling;
global using Silos.Core.Infrastructure.WebApi;
global using Silos.Core.Infrastructure.Identity;
global using Silos.Core.Infrastructure.Integration;
global using Silos.Customers.API.Controllers.Requests;
global using Silos.Customers.Infrastructure.Projections;
global using Silos.Customers.Application.GettingCreditLimit;
global using Silos.Customers.Application.RegisteringCustomer;
global using Silos.Customers.Api.Application.RegisteringCustomer;
global using Silos.Customers.Application.GettingCustomerEventHistory;
global using Silos.Customers.Api.Application.GettingCustomerDetails;