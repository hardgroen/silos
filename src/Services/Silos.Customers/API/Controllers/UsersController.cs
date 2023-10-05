namespace Silos.Users.API.Controllers;

[Authorize]
[Route("api/users")]
[ApiController]
public class UsersController : CustomControllerBase
{
    private ITokenRequester _tokenRequester { get; set; }

    public UsersController(
        ITokenRequester tokenRequester,
        ICommandBus commandBus,
        IQueryBus queryBus)
        : base(commandBus, queryBus)
    {
        _tokenRequester = tokenRequester;
    }

    /// <summary>
    /// Get customer details
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = PolicyBuilder.UserRole, Policy = PolicyBuilder.ReadPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<UserDetails>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDetails() =>
        await Response(
            GetUserDetails.Create(await _tokenRequester.GetUserTokenFromHttpContextAsync())
        );
      

    /// <summary>
    /// Get user event history
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet, Route("{userId::guid}/history")]
    [Authorize(Roles = PolicyBuilder.UserRole, Policy = PolicyBuilder.ReadPolicy)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IList<UserEventHistory>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ListHistory([FromRoute] Guid userId) =>
        await Response(
            GetUserEventHistory.Create(UserId.Of(userId))
        );

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request) =>
        await Response(
            RegisterUser.Create(
                request.Email,
                request.Password,
                request.PasswordConfirm,
                request.Name
            )
        );

    /// <summary>
    /// Update user's information
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut, Route("{userId:guid}")]
    [Authorize(Roles = PolicyBuilder.UserRole, Policy = PolicyBuilder.WritePolicy)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateInformation([FromRoute] Guid userId, [FromBody] UpdateCustomerRequest request) =>
        await Response(
            UpdateUserInformation.Create(
                UserId.Of(userId),
                request.Name
            )
        );
}