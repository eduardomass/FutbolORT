# Sistema Futbol 📖

## Objetivos 📋

Desarrollar un sistema que permita gestionar un grupo de amigos jugando al futbol por semana.\
Utilizar Visual Studio 2019 y crear una aplicación utilizando `ASP.NET MVC Core versión 3.1`.

---------------------------------------

## Enunciado 📢

La idea principal de este trabajo práctico, es que ustedes se comporten como un equipo de desarrollo.\
Deben recopilar todas las dudas que tengan y evacuarlas con su nexo (el docente). 
Es importante destacar que este proceso no debe esperar a ser en clase, sino que debe darse a medida que vayan trabajando en el proyecto. 
Por otro lado es importante que agrupen sus consultas ya sea por criterios funcionales o técnicos y envíen correos con las consultas agrupadas en lugar de enviar cada consulta de forma independiente.

### Consultas

Las consultas que sean realizadas por correo a mailto:eduardo.mass@ort.edu.ar deben seguir el siguiente formato:

**Subject:**

- `[NT1-<CURSO LETRA>-GRP-<GRUPO NUMERO>] <Proyecto XXX> | Informativo o Consulta` *ej: [NT1-A-GRP-5] Agenda de Turnos | Consulta*

**Body:**

1. `<xxxxxxxx>` *ej: ¿Esta bien si usamos validaciones por java script y no razon?*
2. `<xxxxxxxx>` *ej: ¿Está bien que encaremos la validación del turno activo, con una propiedad booleana en el Turno?*

---------------------------------------

## Proceso de ejecución en alto nivel ☑️

- Crear un proyecto utilizando [visual studio].
- Adicionar todos los modelos dentro de la carpeta Models cada uno en un archivo separado.
- Especificar todas las restricciones y validaciones solicitadas a cada una de las entidades. [DataAnnotations].
- Crear las relaciones entre las entidades.
- Crear una carpeta Data que dentro tendrá al menos la clase que representará el contexto de la base de datos DbContext.
- Crear el DbContext utilizando base de datos sqlite. [DbContext], [Database Sqlite], [Db browser sqlite].
- Agregar los DbSet para cada una de las entidades en el DbContext.
- Crear el Scaffolding para permitir los CRUD de las entidades.
- Aplicar las adecuaciones y validaciones necesarias en los controladores.
- Realizar un sistema de login para los roles identificados en el sistema y un administrador.
- Un administrador podrá realizar todas tareas que impliquen interacción del lado del negocio (ABM "Alta-Baja-Modificación" de las entidades del sistema y configuraciones en caso de ser necesarias).
- Cada usuario sólo podrá tomar acción en el sistema en base al rol que tiene.
- Realizar todos los ajustes necesarios en los modelos y/o funcionalidades.
- Realizar los ajustes requeridos del lado de los permisos.
- Todo lo referido a la presentación de la aplicaión (cuestiones visuales).
- Para la visualización se recomienda utilizar [Bootstrap], pero se puede utilizar cualquier herramienta que el grupo considere.

---------------------------------------

## Entidades básicas 📄

- Usuario
- Jugador
- Equipo
- Partido
- Imagen


| Entidad | Propiedades |
| ----- | ----- |
| Usuario | Nombre, Email, Password |
| Jugador | Nombre, Apellido, Dni, Email, Foto, Activo |
| Equipo | Jugadores |
| Partido | Equipo1, Equipo2, Goles1, Goles2, Ganador, Fecha |
| Imagen | Nombre, Path (url) |

**NOTA:** aquí un link para refrescar el uso de los [Data annotations].

---------------------------------------

## Características y Funcionalidades ⌨️

`Todas las entidades deben tener implementado su correspondiente ABM, a menos que sea implícito el no tener que soportar alguna de estas acciones.`


### Jugador

- Los Jugadores no pueden auto activarse, deben ser activador por un Usuario.
- Solo puede loguearse si esta Activo
- El email es obligatorio
- Puede actualizar sus datos
- Puede Confirmar Fecha de Partido en caso de estar Activa

### Partido (Fecha a Confirmar)

**Importante:** `Solo se puede crear un partido por Semana

- La creacion de la Fecha/Partido lo hace solo el usuario
- Al crear un partido, los jugadores se pueden Anotar haciendo login, y confirmando que juegan la proxima Fecha/Partido
- Una vez cumpleatado 10 Jugadores en total no podran anotarse mas.
- Cuando se confirma el partido, pasa a estado de Sorteo


### Proceso de Confirmacion y Sorteo

**Importante:** `Solo se puede sortearse todos los partidos confirmados

- Al sortear, realiza un random de todos los jugadores armando los 2 equipos. (Por ahora sin logica)

### Partido (Sorteado)
- Los partidos sorteados pueden editar quien Gano, Equipo 1 , Equipo 2 o Empate poniendo el resultado directamente.
- Al poner el resultado el campo Ganador se autocompleta poniendo 0, 1 o 2

### Tabla de Posiciones

- Mostrar todos los Jugadores por Ranking de Partidos Ganados, Perdidos, Empatados
- El puntaje de cada jugador se calcula como 3 puntos ganaddo, 2 Puntos Empatado, 1 Punto perdido.


[//]: # (referencias externas)
   [visual studio]: <https://visualstudio.microsoft.com/en/vs/>
   [Data annotations]: <https://www.c-sharpcorner.com/UploadFile/af66b7/data-annotations-for-mvc/>
   [Bootstrap]: <https://getbootstrap.com/>
   [DbContext]: <https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-3.1>
   [Database Sqlite]: <https://docs.microsoft.com/en-us/ef/core/providers/sqlite/?tabs=dotnet-core-cli>
   [Db browser sqlite]: <https://sqlitebrowser.org/>
   [DataAnnotations]: <https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1>