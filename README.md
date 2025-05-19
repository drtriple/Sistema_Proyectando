# Proyecto Proyectando

**Caso:** La empresa de desarrollo de software "Proyectando" desea tener el control de sus programadores que realizan
desarrollos para sus clientes, el desarrollo incluye todas las fases del ciclo de vida del software: (Levantamiento de
requerimientos, análisis y diseño de la solución, construcción (desarrollo) de la solución y pruebas). En un proyecto
pueden trabajar varios programadores y un programador puede trabajar en varios proyectos, el control se basa en saber
que fase realiza cada programador en cada proyecto, el tiempo que se gasta y el estado en que se encuentra el
desarrollo de cada fase. Un programador puede realizar un mismo proceso en dias diferentes. Cada fase puede tener
varios procesos. Al final de cada desarrollo debe de mostrar el tiempo empleado para su implementación en cada uno
de las fases, los programadores que participaron y el tiempo empleado en cada proceso. Debe de existir la forma de
registrar a los usuarios del sistema (con clave y contraseña) y la captura del empleado que realiza cada proceso. Un
cliente puede tener uno o varios teléfonos y una o varias direcciones en diferentes ciudades, lo mismo el programador.
Los datos en este enunciado son básicos, los deben de complementar acorde a las consultas realizadas acorde
problema propuesto.

## Diagrama de Clases UML
![form](https://raw.githubusercontent.com/drtriple/Sistema_Proyectando/refs/heads/main/Diagrama.png)

### > Explicación de las relaciones

#### **1. Relación:** Cliente – DireccionCliente 

**Tipo:** Uno a muchos

**Explicación:** Un cliente puede tener varias direcciones (por ejemplo, oficinas en diferentes ciudades), pero cada dirección pertenece a un único cliente.

#### **2. Relación:** Cliente – TelefonoCliente

**Tipo:** Uno a muchos

**Explicación:** Un cliente puede tener varios números telefónicos registrados, pero cada número está asociado con un solo cliente.

#### **3. Relación:** Programador – DireccionProgramador

**Tipo:** Uno a muchos

**Explicación:** Un programador puede vivir en distintas ciudades a lo largo del tiempo (o tener direcciones de contacto), y cada dirección pertenece a un único programador.

#### **4. Relación:** Programador – TelefonoProgramador

**Tipo:** Uno a muchos

**Explicación:** Cada programador puede tener varios teléfonos de contacto, como celular y oficina.

#### **5. Relación:** Programador – Usuario

**Tipo:** Uno a uno (opcional en Usuario)

**Explicación:** Cada usuario del sistema está relacionado con un programador, quien es el que realiza y registra actividades en el sistema. No todos los programadores necesitan tener usuario si no acceden directamente al sistema.

#### **6. Relación:** Cliente – Proyecto

**Tipo:** Uno a muchos

**Explicación:** Un cliente puede contratar varios proyectos de desarrollo, pero cada proyecto pertenece a un solo cliente.

#### **7. Relación:** Proyecto – Programador_Proyecto

**Tipo:** Muchos a muchos (resuelta con tabla intermedia)

**Explicación:** Varios programadores pueden participar en un proyecto, y un programador puede trabajar en varios proyectos.

#### **8. Relación:** Programador – Programador_Proyecto

**Tipo:** Muchos a muchos (resuelta con tabla intermedia)

**Explicación:** Es la contraparte de la relación anterior. Permite saber qué programadores están asignados a qué proyectos.

#### **9. Relación:** Fase – Proceso

**Tipo:** Uno a muchos

**Explicación:** Cada fase del ciclo de vida del software (como análisis, pruebas, etc.) contiene uno o varios procesos específicos.

#### 10. RegistroProceso
**Relaciones:**

RegistroProceso --> Programador

RegistroProceso --> Proyecto

RegistroProceso --> Proceso

**Tipo:** Muchos a uno (cada relación)

**Explicación:** Esta entidad registra el trabajo que un programador realiza en un proceso específico de un proyecto. Incluye información como la fecha, tiempo empleado y estado.
Permite múltiples registros del mismo proceso en distintas fechas, como se solicita en el problema.

## Requisitos Funcionales

**RF01 – Registro de Clientes**: El sistema debe permitir registrar clientes con sus datos personales, múltiples direcciones y teléfonos.

**RF02 – Registro de Programadores**: El sistema debe permitir registrar programadores, incluyendo múltiples direcciones y números de contacto.

**RF03 – Autenticación de Usuarios**: El sistema debe permitir a los programadores acceder mediante usuario y contraseña para registrar su trabajo.

**RF04 – Gestión de Proyectos**: El sistema debe permitir registrar proyectos asociados a un cliente, incluyendo fechas, descripción y participantes.

**RF05 – Gestión de Fases y Procesos**: El sistema debe permitir definir fases del ciclo de vida del software y asociar múltiples procesos a cada fase.

**RF06 – Asignación de Programadores a Proyectos**: El sistema debe permitir asignar uno o más programadores a uno o más proyectos.

**RF07 – Registro de Actividades de Programadores**: El sistema debe registrar qué programador realizó qué proceso, en qué proyecto, con fecha, duración y estado del trabajo.

**RF08 – Consulta de Participación y Tiempos**: El sistema debe mostrar los programadores involucrados en un proyecto, los procesos realizados y el tiempo empleado por cada uno.

**RF09 – Control de Estado de Fases y Procesos**: El sistema debe permitir registrar el estado de cada proceso dentro de cada fase (ej. pendiente, en progreso, completado).

## Requisitos No Funcionales

**RNF01 – Plataforma de Desarrollo**: El sistema debe ser desarrollado en .NET Framework.

**RNF02 – Persistencia de Datos**: El sistema debe utilizar una base de datos relacional SQL Server para almacenar la información.

**RNF03 – Interfaz Amigable**: El sistema debe contar con una interfaz simple y clara, adecuada para propósitos educativos.

**RNF04 – Seguridad Básica**: El sistema debe implementar autenticación simple con validación de usuario y contraseña.

**RNF05 – Modularidad del Código**: El código debe estar organizado en capas mediante un Frontend tipo Aplicación Web .NET y un Backend tipo API Web .NET, aplicando buenas prácticas.

## Requisitos Técnicos

**Versión:** .NET Framework 4.7.2

**Visual Studio:** 2022

**Framework:** Bootstrap 5

**Base de datos:** SQL SERVER 