FROM mcr.microsoft.com/dotnet/core/sdk
WORKDIR /app
COPY . ./
RUN dotnet publish SkillSearch/SkillSearch.csproj -c Release -o SkillSearch/publish
WORKDIR /app/SkillSearch
ENTRYPOINT ["dotnet", "publish/SkillSearch.dll"]
