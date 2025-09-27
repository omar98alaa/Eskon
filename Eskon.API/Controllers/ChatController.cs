using Eskon.API.Base;
using Eskon.Core.Features.ChatFeatures.Commands.Command;
using Eskon.Core.Features.ChatFeatures.Queries.Query;
using Eskon.Core.Response;
using Eskon.Domian.DTOs.Chat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eskon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : BaseController
    {
        #region Fields
        private readonly IMediator _mediator;
        #endregion

        #region Constructors
        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region GET
        #region GET all messages for chat
        /// <summary>
        /// Retrieves all messages for a specified chat.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint returns all chat messages associated with a given chat ID, but only if the requesting user is a participant in that chat.
        ///
        /// **Workflow:**  
        /// 1. Extracts the authenticated user ID from the JWT token.  
        /// 2. Validates that the chat exists.  
        /// 3. Ensures the requesting user is either `User1` or `User2` in the chat.  
        /// 4. Maps and returns all messages as a list of `ChatMessageDto`.
        ///
        /// **Authorization:**  
        /// - Requires authentication.  
        /// - User must be a participant in the chat.
        ///
        /// **Example Request:**
        /// ```http
        /// GET /api/Chat/messages/9f21f36b-0f34-4f61-bc66-a6e918ddc87e
        /// Authorization: Bearer {token}
        /// ```
        ///
        ///
        /// **Possible Error Responses:**  
        /// - `404 Not Found` — If the chat does not exist.  
        /// - `403 Forbidden` — If the requesting user is not a participant in the chat.  
        /// - `500 Internal Server Error` — If an unhandled exception occurs.
        ///
        /// **Notes:**  
        /// - Only participants of the chat can retrieve messages.  
        /// - Messages are returned in the order they are stored in the database (consider sorting by `SentAt` if needed).
        /// </remarks>
        /// <param name="chatId">The unique identifier of the chat.</param>
        /// <returns>A list of messages in the specified chat.</returns>
        /// <response code="200">Messages retrieved successfully.</response>
        /// <response code="403">User is not authorized to access this chat.</response>
        /// <response code="404">Chat not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("messages/{chatId:guid}")]
        [ProducesResponseType(typeof(Response<List<ChatMessageDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<ChatMessageDto>>), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(Response<List<ChatMessageDto>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<List<ChatMessageDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMessages(Guid chatId)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var result = await _mediator.Send(new GetMessagesPerChatQuery(userId, chatId));
            return NewResult(result);
        }
        #endregion

        #region GET all chats for user
        /// <summary>
        /// Retrieves all chat conversations for the authenticated user.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint returns a list of all conversations (chats) the currently logged-in user is part of.  
        /// Each conversation includes participant details and metadata about the latest messages.
        ///
        /// **Workflow:**  
        /// 1. Extracts the authenticated user ID from the JWT token.  
        /// 2. Fetches all chat records for that user.  
        /// 3. Maps them to `ConversationDto` objects.  
        /// 4. Returns the list in the response.
        ///
        /// **Authorization:**  
        /// - Requires authentication.  
        /// - Only returns conversations belonging to the authenticated user.
        ///
        /// **Example Request:**
        /// ```http
        /// GET /api/Chat/conversations
        /// Authorization: Bearer {token}
        /// ```
        ///
        /// **Possible Error Responses:**  
        /// - `401 Unauthorized` — If no valid token is provided.  
        /// - `500 Internal Server Error` — If an unexpected error occurs.
        ///
        /// **Notes:**  
        /// - The returned list may be empty if the user has no active conversations.  
        /// - Ordering of conversations is typically by most recent message, but depends on service implementation.
        /// </remarks>
        /// <returns>List of conversations for the authenticated user.</returns>
        /// <response code="200">Conversations retrieved successfully.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("conversations")]
        [ProducesResponseType(typeof(Response<List<ConversationDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<List<ConversationDto>>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<List<ConversationDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserConversations()
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var result = await _mediator.Send(new GetUserConversationsQuery(userId));
            return NewResult(result);
        }
        #endregion
        #endregion

        #region POST
        #region Mark messages as read
        /// <summary>
        /// Marks all unread messages in a chat as read for the authenticated user.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint updates the read status of all unread messages in the specified chat,  
        /// provided that the authenticated user is a participant in the chat.
        ///
        /// **Workflow:**  
        /// 1. Extracts the authenticated user's ID from the JWT token.  
        /// 2. Validates that the chat exists.  
        /// 3. Ensures that the user is a participant in the chat.  
        /// 4. Marks all unread messages as read.  
        /// 5. Returns a success response with no payload.
        ///
        /// **Authorization:**  
        /// - Requires authentication.  
        /// - Only the two participants in the chat can mark its messages as read.
        ///
        /// **Example Request:**
        /// ```http
        /// POST /api/Chat/mark-as-read/3d6f0a4e-9b5f-4d78-8d90-0f1e89e76f3a
        /// Authorization: Bearer {token}
        /// ```
        ///
        ///
        /// **Possible Error Responses:**  
        /// - `401 Unauthorized` — If no valid token is provided.  
        /// - `403 Forbidden` — If the authenticated user is not a participant in the chat.  
        /// - `404 Not Found` — If the specified chat does not exist.  
        /// - `500 Internal Server Error` — If an unexpected error occurs.
        ///
        /// **Notes:**  
        /// - This operation does not return the updated messages list — it only updates their read status.
        /// - The client should refresh the chat view to see updated read receipts.
        /// </remarks>
        /// <param name="chatId">The unique identifier of the chat whose messages are to be marked as read.</param>
        /// <returns>A success response indicating the messages have been marked as read.</returns>
        /// <response code="200">Messages marked as read successfully.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="403">User is not a participant in this chat.</response>
        /// <response code="404">Chat not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("mark-as-read/{chatId}")]
        [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarkMessagesAsRead(Guid chatId)
        {
            var userId = GetUserIdFromAuthenticatedUserToken();
            var response = await _mediator.Send(new MarkMessagesAsReadCommand(chatId, userId));
            return NewResult(response);
        }
        #endregion

        #region Create a new conversation
        /// <summary>
        /// Creates a new chat conversation between the authenticated user and another specified user.
        /// </summary>
        /// <remarks>
        /// **Description:**  
        /// This endpoint creates a new chat if one does not already exist between the authenticated user and the target user.  
        /// If the chat already exists, the existing conversation details are returned.
        ///
        /// **Workflow:**  
        /// 1. Extracts the authenticated user's ID from the JWT token.  
        /// 2. Validates that the authenticated user is not trying to start a chat with themselves.  
        /// 3. Checks if the other user exists in the system.  
        /// 4. Checks if a chat already exists between the two users.  
        ///     - If no chat exists, a new one is created.  
        ///     - If a chat exists, that chat is returned.  
        /// 5. Returns conversation details.
        ///
        /// **Authorization:**  
        /// - Requires authentication.  
        /// - Both participants must be registered users.
        ///
        /// **Example Request:**
        /// ```http
        /// POST /api/Chat/5b9d4f6a-2a7f-4c65-94b6-7b8bffaf4c9d
        /// Authorization: Bearer {token}
        /// ```
        ///
        ///
        /// **Possible Error Responses:**  
        /// - `400 Bad Request` — If the authenticated user tries to start a chat with themselves.  
        /// - `401 Unauthorized` — If no valid token is provided.  
        /// - `404 Not Found` — If the other user does not exist.  
        /// - `500 Internal Server Error` — If an unexpected error occurs.
        ///
        /// **Notes:**  
        /// - If a chat already exists between the two users, this will return that existing chat rather than creating a new one.
        /// - Clients can use this endpoint as an "open or create" chat operation.
        /// </remarks>
        /// <param name="user2Id">The unique identifier of the other user to start a chat with.</param>
        /// <returns>A conversation DTO containing chat details.</returns>
        /// <response code="201">Chat created successfully.</response>
        /// <response code="200">Chat already existed and is returned successfully.</response>
        /// <response code="400">Cannot create a conversation with yourself.</response>
        /// <response code="401">User is not authenticated.</response>
        /// <response code="404">Other user does not exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("{user2Id:guid}")]
        [ProducesResponseType(typeof(Response<ConversationDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Response<ConversationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<ConversationDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response<ConversationDto>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response<ConversationDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Response<ConversationDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewChat([FromRoute] Guid user2Id)
        {
            var user1Id = GetUserIdFromAuthenticatedUserToken();
            var response = await _mediator.Send(new AddNewChatCommand(user1Id, user2Id));
            return NewResult(response);
        }  
        #endregion
        #endregion
    }
}
