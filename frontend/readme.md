Proyecto de Frontend para Diners Club Online
============================================

Requerimientos
--------------

1. Instalar:
  - [NodeJS](https://nodejs.org/en/)

2. Ejecutar en PowerShell:
  ```
  npm install --global gulp-cli bower typings
  ```
  ```
  npm install
  ```
  > Ejecutar este último comando cada vez que alguien agregue una nueva dependencia en el proyecto (bien sea de NPM, Bower o TypeScript).

Ejecución de tareas
-------------------

- Para poder ver qué tareas se tienen
  ```
  gulp --tasks-simple
  ```

Levantar el proyecto
--------------------
- Para poder desarrollar:
  ```
  gulp serve
  ```
  Cada vez que se cambie algún archivo de HTML, TypeScript o SASS, se regenerará el output y se refrescará automáticamente el browser.
- Para levantar el proyecto que será generado para un entorno remoto y verlo en el browser localmente:
  ```
  gulp serve:local
  ```
- Para levantar el proyecto que será generado para el entorno `dev` y verlo en el browser:
  ```
  gulp serve:dev
  ```
- Para levantar el proyecto que será generado para el entorno `qa` y verlo en el browser:
  ```
  gulp serve:dist
  ```

Generación del proyecto
-----------------------
- Para poder generar el proyecto para el entorno `dev`:
  ```
  gulp build:dev
  ```
- Para poder generar el proyecto para el entorno `qa`:
  ```
  gulp
  ```

> Los archivos generados se encuentran en el fólder `dist`.
