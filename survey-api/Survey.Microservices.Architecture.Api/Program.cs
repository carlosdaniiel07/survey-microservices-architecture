using Survey.Microservices.Architecture.Api.Infraestructure;
using Survey.Microservices.Architecture.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Configure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Survey.Microservices.Architecture.Api");
});
app.UseMiddleware<GlobalErrorHandlerMiddleware>();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
