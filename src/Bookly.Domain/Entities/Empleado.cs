using Bookly.Domain.Common;
using Bookly.Domain.ValueObjects;

namespace Bookly.Domain.Entities;

public sealed class Empleado : BaseEntity
{
    public FullName NombreCompleto { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public PhoneNumber Telefono { get; private set; } = null!;

    private readonly List<Servicio> _servicios = new();
    public IReadOnlyCollection<Servicio> Servicios => _servicios;

    private readonly List<EmpleadoHorarioLaboral> _horariosLaborales = [];
    public IReadOnlyCollection<EmpleadoHorarioLaboral> HorariosLaborales => _horariosLaborales;

    private Empleado()
    {
        // Constructor privado para EF Core
    }

    private Empleado(FullName nombreCompleto, Email email, PhoneNumber telefono)
    {
        ArgumentNullException.ThrowIfNull(nombreCompleto);
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(telefono);

        NombreCompleto = nombreCompleto;
        Email = email;
        Telefono = telefono;
    }

    public static Empleado Create(FullName nombreCompleto, Email email, PhoneNumber telefono)
    {
        return new Empleado(nombreCompleto, email, telefono);
    }

    public void Activar()
    {
        if (State)
            return;

        State = true;
    }

    public void Desactivar()
    {
        if (!State)
            return;

        State = false;
    }

    public void CambiarEmail(Email nuevoEmail)
    {
        ArgumentNullException.ThrowIfNull(nuevoEmail);

        if (Email.Equals(nuevoEmail))
            return;

        Email = nuevoEmail;
    }

    public void CambiarTelefono(string nuevoTelefono)
    {
        if (string.IsNullOrWhiteSpace(nuevoTelefono))
            throw new ArgumentException("El telefono no puede estar vacio");

        Telefono = PhoneNumber.Create(nuevoTelefono);
    }

    public void CambiarNombreCompleto(FullName nuevoNombreCompleto)
    {
        ArgumentNullException.ThrowIfNull(nuevoNombreCompleto);

        if (NombreCompleto.Equals(nuevoNombreCompleto))
            return;

        NombreCompleto = nuevoNombreCompleto;
    }

    public void AsignarServicio(Servicio servicio)
    {
        ArgumentNullException.ThrowIfNull(servicio);

        if (_servicios.Any(x => x.Id == servicio.Id))
            return;

        _servicios.Add(servicio);
    }

    public void QuitarServicio(Servicio servicio)
    {
        ArgumentNullException.ThrowIfNull(servicio);

        var existente = _servicios.FirstOrDefault(x => x.Id == servicio.Id);
        if (existente is null)
            return;

        _servicios.Remove(existente);
    }

    public bool PuedeRealizarServicio(Servicio servicio)
    {
        ArgumentNullException.ThrowIfNull(servicio);
        return _servicios.Any(x => x.Id == servicio.Id);
    }

    public bool PuedeRealizarServicio(Guid servicioId)
    {
        return _servicios.Any(x => x.Id == servicioId);
    }

    public void AsignarHorarioLaboral(HorarioLaboral horarioLaboral)
    {
        ArgumentNullException.ThrowIfNull(horarioLaboral);

        if (_horariosLaborales.Any(x => x.HorarioLaboralId == horarioLaboral.Id))
            return;

        _horariosLaborales.Add(EmpleadoHorarioLaboral.Create(Id, horarioLaboral));
    }

    public void QuitarHorarioLaboral(HorarioLaboral horarioLaboral)
    {
        ArgumentNullException.ThrowIfNull(horarioLaboral);

        var existente = _horariosLaborales.FirstOrDefault(x => x.HorarioLaboralId == horarioLaboral.Id);
        if (existente is null)
            return;

        _horariosLaborales.Remove(existente);
    }

    public bool TieneHorarioLaboral(HorarioLaboral horarioLaboral)
    {
        ArgumentNullException.ThrowIfNull(horarioLaboral);
        return _horariosLaborales.Any(x => x.HorarioLaboralId == horarioLaboral.Id);
    }

    public bool TieneHorarioLaboral(Guid horarioLaboralId)
    {
        return _horariosLaborales.Any(x => x.HorarioLaboralId == horarioLaboralId);
    }
}
