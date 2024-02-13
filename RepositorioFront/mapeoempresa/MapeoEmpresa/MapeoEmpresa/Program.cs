using MapeoEmpresa.Services;
using MySqlConnector;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using EntidadesNegocio.ClasesDao.ElementosDAO;
using EntidadesNegocio.ClasesDao.TercerosDAO;
using EntidadesNegocio.ClasesDao.CatalogoDAO;
using EntidadesNegocio.ClasesDao.ProcedenciaDAO;
using EntidadesNegocio.ClasesDao.ImpuestosYDescuentosDAO;
using ServiciosComponentes.InventarioServices;
using terceros = ServiciosComponentes.TercerosServices;
using ServiciosComponentes.ProcedenciaServices;
using ServiciosComponentes.ImpuestosYDescuentosServices;
using ServiciosComponentes.VentaTradicionalService;
using ServiciosComponentes.UsuarioServices;
using CatalogoVentaOnline.ListadoCarritoCompras;
using EntidadesNegocio.ClasesDao.UsuarioDAO;
using EntidadesNegocio.VentaOnlineTradicional;
using EntidadesNegocio.ClasesDao.Ventas;
using Microsoft.AspNetCore.ResponseCompression;
using MapeoEmpresa.Hubs;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

// Registra el servicio de base de datos en el contenedor de inyeccion de dependencias 
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

builder.Services.AddSingleton<MySqlConnection>(_ => new MySqlConnection(connectionString));


//esquema de autenticacion
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", option => {
    option.Cookie.Name = "MyCookieAuth";
    option.LoginPath = "/login"; //la ruta donde está el login 
    option.AccessDeniedPath = "/accesoDenegado";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
});

//servicios para chat 
builder.Services.AddResponseCompression(opts =>
{ 
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream"});
});


//politicas de acceso
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("PoliticaCreacionEmpresas",
        policy => policy.RequireClaim("CREAR_EMPRESAS"));
    options.AddPolicy("PoliticaActualizacionEmpresas",
        policy => policy.RequireClaim("ACTUALIZAR_EMPRESAS"));
    options.AddPolicy("PoliticaFiltradoEmpresas", 
        policy => policy.RequireClaim("FILTRAR_EMPRESAS"));
    options.AddPolicy("PoliticaCreacionClientes",
        policy => policy.RequireClaim("CREAR_CLIENTES"));
    options.AddPolicy("PoliticaActualizacionClientes",
        policy => policy.RequireClaim("ACTUALIZAR_CLIENTES"));
    options.AddPolicy("PoliticaFiltradoClientes",
        policy => policy.RequireClaim("FILTRAR_CLIENTES"));
    options.AddPolicy("PoliticaCreacionEmpleados",
        policy => policy.RequireClaim("CREAR_EMPLEADOS"));
    options.AddPolicy("PoliticaActualizacionEmpleados",
        policy => policy.RequireClaim("ACTUALIZAR_EMPLEADOS"));
    options.AddPolicy("PoliticaFiltradoEmpleados",
        policy => policy.RequireClaim("FILTRAR_EMPLEADOS"));
    options.AddPolicy("PoliticaConfiguracionImpuestos",
        policy => policy.RequireClaim("CONFIGURAR_IMPUESTOS"));
    options.AddPolicy("PoliticaConfiguracionDescuentos",
        policy => policy.RequireClaim("CONFIGURAR_DESCUENTOS"));
    options.AddPolicy("PoliticaRealizacionVentas",
        policy => policy.RequireClaim("REALIZAR_VENTAS"));
    options.AddPolicy("PoliticaConfiguracionPermisosEmpleados",
        policy => policy.RequireClaim("CONFIGURAR_PERMISOS_EMPLEADOS"));
    options.AddPolicy("PoliticaConfiguracionPermisosEmpresas",
        policy => policy.RequireClaim("CONFIGURAR_PERMISOS_EMPRESAS"));
    options.AddPolicy("PoliticaConsultaArticulosVendidosCliente",
        policy => policy.RequireClaim("CONSULTAR_ARTICULOS_VENDIDOS_CLIENTE"));
    options.AddPolicy("PoliticaConsultaHistorialVentasArticulo",
        policy => policy.RequireClaim("CONSULTAR_HISTORIAL_VENTAS_ARTICULO"));
    options.AddPolicy("PoliticaConsultaHistorialVentasCliente",
        policy => policy.RequireClaim("CONSULTAR_HISTORIAL_VENTAS_CLIENTE"));
    options.AddPolicy("PoliticaConversionTipoVenta",
        policy => policy.RequireClaim("REALIZAR_CONVERSION_TIPO_VENTA"));
    options.AddPolicy("PoliticaFiltradoElementos",
        policy => policy.RequireClaim("FILTRAR_ELEMENTOS"));
    options.AddPolicy("PoliticaActualizacionElementos",
        policy => policy.RequireClaim("ACTUALIZAR_ELEMENTOS"));
    options.AddPolicy("PoliticaCreacionElementos",
        policy => policy.RequireClaim("CREAR_ELEMENTOS"));
    options.AddPolicy("PoliticaAgendarVisita",
        policy => policy.RequireClaim("AGENDAR_VISITA"));
    options.AddPolicy("PoliticaRealizarVisita",
        policy => policy.RequireClaim("REALIZAR_VISITA"));
    options.AddPolicy("PoliticaVerMapaProcesos",
        policy => policy.RequireClaim("VER_MAPA_PROCESOS"));
    options.AddPolicy("PoliticaDefinirEmpresa",
        policy => policy.RequireClaim("DEFINIR_EMPRESA"));
    options.AddPolicy("PoliticaVerInventario",
        policy => policy.RequireClaim("VER_INVENTARIO"));
    options.AddPolicy("PoliticaAgendarRevisionInventario",
        policy => policy.RequireClaim("AGENDAR_REVISION_INVENTARIO"));
    options.AddPolicy("PoliticaRealizarRevisionInventario",
        policy => policy.RequireClaim("REALIZAR_REVISION_INVENTARIO"));
    options.AddPolicy("PoliticaInformesRevisionInventario",
        policy => policy.RequireClaim("VER_INFORMES_REVISION_INVENTARIO"));
    options.AddPolicy("PoliticaAsociarVentaVisita",
        policy => policy.RequireClaim("ASOCIAR_VENTA_VISITA"));
    options.AddPolicy("PoliticaInformesVisita",
        policy => policy.RequireClaim("VER_INFORMES_VISITA"));

});



builder.Services.AddScoped<HttpClient>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<MapaProcesosService>();
builder.Services.AddSingleton<SedeService>();
builder.Services.AddSingleton<PlantaService>();
builder.Services.AddSingleton<AreaService>();
builder.Services.AddSingleton<ComponenteService>();
builder.Services.AddSingleton<EquipoService>();
builder.Services.AddSingleton<ParteService>();
builder.Services.AddSingleton<CondicionOperativaInicialService>();
builder.Services.AddSingleton<RegistroEvidenciaService>();
builder.Services.AddSingleton<RevisionInventarioService>();
builder.Services.AddSingleton<VisitaService>();
builder.Services.AddSingleton<CondicionOperativaRealService>();


//servicios grupo 1
builder.Services.AddScoped<ElementoDAO>();
builder.Services.AddScoped<EmpresaDAO>();
builder.Services.AddScoped<TiposDocumentoDAO>();
builder.Services.AddScoped<PaisDAO>();
builder.Services.AddScoped<DepartamentoDAO>();
builder.Services.AddScoped<MunicipioDAO>();
builder.Services.AddScoped<SedeDAO>();
builder.Services.AddScoped<ImpuestoDAO>();
builder.Services.AddScoped<ClienteDAO>();
builder.Services.AddScoped<EmpleadoDAO>();
builder.Services.AddScoped<UsuarioDAO>();
builder.Services.AddScoped<DescuentoDAO>();


builder.Services.AddScoped<CatalogoService> ();
builder.Services.AddScoped<terceros.ClienteService>();
builder.Services.AddScoped<terceros.EmpleadoService>();
builder.Services.AddScoped<terceros.EmpresaService>();
builder.Services.AddScoped<PaisService>();
builder.Services.AddScoped<DepartamentoService>();
builder.Services.AddScoped<ciudadService>();
builder.Services.AddScoped<terceros.SedeServices>();
builder.Services.AddScoped<terceros.TipoDocumentoService>();
builder.Services.AddScoped<CatalogoDAO>();
builder.Services.AddScoped<VentaTradicionalDAO>();
builder.Services.AddScoped<VentaTradicionalService>();

builder.Services.AddScoped<ImpuestoService>();
builder.Services.AddScoped<ElementoService>();
builder.Services.AddScoped<VentaService>();
builder.Services.AddScoped<Venta,Cotizacion>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<DescuentoService>();

builder.Services.AddScoped<ListadoCarritoCompras>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapBlazorHub();
app.MapControllers();
app.MapHub<ChatHub>("/chathub");
app.MapFallbackToPage("/_Host");


app.Run();
