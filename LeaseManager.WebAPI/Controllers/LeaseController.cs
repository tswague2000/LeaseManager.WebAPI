using LeaseManager.WebAPI.Application.Common.Interfaces;
using LeaseManager.WebAPI.Common.Responses;
using Microsoft.AspNetCore.Mvc;
using static LeaseManager.WebAPI.Application.DTOs.LeaseDTOs;

namespace LeaseManager.WebAPI.Controllers
{
    /// <summary>
    /// Contrôleur pour la gestion des contrats de location
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class LeaseController : ControllerBase
    {
        private readonly ILeaseService _leaseService;
        private readonly ILogger<LeaseController> _logger;

        public LeaseController(ILeaseService leaseService, ILogger<LeaseController> logger)
        {
   _leaseService = leaseService;
         _logger = logger;
}

      /// <summary>
        /// Récupère tous les contrats de location
        /// </summary>
    /// <returns>Liste des baux</returns>
    [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ApiResponse<IEnumerable<LeaseReadDto>>>> GetAll()
     {
  try
         {
      _logger.LogInformation("Récupération de tous les baux");
           var leases = await _leaseService.GetAllAsync();
    return Ok(ApiResponse<IEnumerable<LeaseReadDto>>.SuccessResponse(leases, "Baux récupérés avec succès"));
         }
      catch (Exception ex)
       {
      _logger.LogError(ex, "Erreur lors de la récupération des baux");
   return StatusCode(StatusCodes.Status500InternalServerError, 
      ApiResponse<LeaseReadDto>.ErrorResponse("Erreur serveur interne"));
            }
        }

        /// <summary>
        /// Récupère un contrat de location par ID
        /// </summary>
   /// <param name="id">ID du bail</param>
     /// <returns>Le bail demandé</returns>
        [HttpGet("{id}")]
   [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<ApiResponse<LeaseReadDto>>> GetById(int id)
    {
  try
          {
      if (id <= 0)
    return BadRequest(ApiResponse<LeaseReadDto>.ErrorResponse("L'ID doit être supérieur à 0"));

        _logger.LogInformation($"Récupération du bail avec l'ID {id}");
          var lease = await _leaseService.GetByIdAsync(id);
       if (lease == null)
    return NotFound(ApiResponse<LeaseReadDto>.ErrorResponse("Bail non trouvé"));

   return Ok(ApiResponse<LeaseReadDto>.SuccessResponse(lease, "Bail récupéré avec succès"));
    }
            catch (Exception ex)
        {
        _logger.LogError(ex, $"Erreur lors de la récupération du bail {id}");
    return StatusCode(StatusCodes.Status500InternalServerError, 
     ApiResponse<LeaseReadDto>.ErrorResponse("Erreur serveur interne"));
            }
     }

/// <summary>
 /// Crée un nouveau contrat de location
  /// </summary>
     /// <param name="dto">Données du nouveau bail</param>
   /// <returns>Le bail créé</returns>
    [HttpPost]
       [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
      public async Task<ActionResult<ApiResponse<LeaseReadDto>>> Create(LeaseCreateDto dto)
        {
         try
            {
    _logger.LogInformation("Création d'un nouveau bail");
      var lease = await _leaseService.CreateAsync(dto);
      return CreatedAtAction(nameof(GetById), new { id = lease.Id }, 
   ApiResponse<LeaseReadDto>.SuccessResponse(lease, "Bail créé avec succès"));
       }
  catch (InvalidOperationException ex)
     {
       _logger.LogWarning(ex, "Erreur de validation lors de la création du bail");
          return BadRequest(ApiResponse<LeaseReadDto>.ErrorResponse(ex.Message));
       }
         catch (Exception ex)
 {
      _logger.LogError(ex, "Erreur lors de la création du bail");
    return StatusCode(StatusCodes.Status500InternalServerError, 
 ApiResponse<LeaseReadDto>.ErrorResponse("Erreur serveur interne"));
        }
        }

      /// <summary>
      /// Met à jour un contrat de location existant
      /// </summary>
       /// <param name="id">ID du bail</param>
     /// <param name="dto">Données mise à jour</param>
    /// <returns>Pas de contenu</returns>
        [HttpPut("{id}")]
      [ProducesResponseType(StatusCodes.Status204NoContent)]
       [ProducesResponseType(StatusCodes.Status404NotFound)]
      [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Update(int id, LeaseUpdateDto dto)
    {
      try
       {
  if (id <= 0)
              return BadRequest(ApiResponse<object>.ErrorResponse("L'ID doit être supérieur à 0"));

  _logger.LogInformation($"Mise à jour du bail {id}");
   var success = await _leaseService.UpdateAsync(id, dto);
         if (!success)
       return NotFound(ApiResponse<object>.ErrorResponse("Bail non trouvé"));

  _logger.LogInformation($"Bail {id} mis à jour avec succès");
    return NoContent();
}
            catch (Exception ex)
         {
 _logger.LogError(ex, $"Erreur lors de la mise à jour du bail {id}");
   return StatusCode(StatusCodes.Status500InternalServerError, 
      ApiResponse<object>.ErrorResponse("Erreur serveur interne"));
 }
   }

        /// <summary>
/// Supprime un contrat de location
        /// </summary>
    /// <param name="id">ID du bail</param>
     /// <returns>Pas de contenu</returns>
        [HttpDelete("{id}")]
       [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
     [ProducesResponseType(StatusCodes.Status400BadRequest)]
 public async Task<ActionResult> Delete(int id)
  {
  try
            {
      if (id <= 0)
     return BadRequest(ApiResponse<object>.ErrorResponse("L'ID doit être supérieur à 0"));

      _logger.LogInformation($"Suppression du bail {id}");
     var success = await _leaseService.DeleteAsync(id);
         if (!success)
 return NotFound(ApiResponse<object>.ErrorResponse("Bail non trouvé"));

    _logger.LogInformation($"Bail {id} supprimé avec succès");
   return NoContent();
}
   catch (Exception ex)
  {
    _logger.LogError(ex, $"Erreur lors de la suppression du bail {id}");
        return StatusCode(StatusCodes.Status500InternalServerError, 
 ApiResponse<object>.ErrorResponse("Erreur serveur interne"));
          }
   }
  }
}