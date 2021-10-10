# DAA-Backend
Aplicación backend desarrollada con Asp.Net Core para el proyecto "DataTables - Asp.Net Core - Angular".
<br />

# 1. Ejecutar migraciones
<p><b>Antes de realizar este paso es necesario tener instalado Sql Server y tener el fichero "databaseSettings.json" del proyecto "DAA.Database.Migrations" bien configurado con vuestros datos de conexión de la base de datos.</b></p>
<p>Una vez descargado o clonado el repositorio hay que abrirlo con Visual Studio y realizar los siguientes pasos:</p>
<ul>
	<li>1.- Establecer el proyecto "DAA.Database.Migrations" como proyecto de inicio.</li>
	<li>2.- Abrir la consola de administrador de paquetes (Ver -> Otras ventanas -> Consola del Administrador de paquetes).</li>
	<li>3.- En la consola de administrador de paquetes hay que establecer el "Proyecto predeterminado" al proyecto "DAA.Database.Migrations" (para ello no hace falta ejecutar ningún comando, la propia consola ya ofrece un campo visual para cambiar esta opción.)</li>
	<li>4.- En la consola de administrador de paquetes ejecutar el comando <b><i>Update-Database</i></b>.</li>
</ul>
<p>Al ejecutar estos pasos se os habrá creado la base de datos que tenéis especificada en el fichero de configuración "databaseSettings.json" en el proyecto "DAA.Database.Migrations" (en mi caso se ha creado la base de datos "DAA_Test").</p>
<br />

# 2. Ejecutar semillas
<p><b>Antes de realizar este paso es necesario haber realizar el paso 1 (haber ejecutado las migraciones para crear la base de datos).</b></p>
<p>Para ejecutar las semillas tenemos que realizar los siguientes pasos:</p>
<ul>
	<li>1.- Ejecutar el proyecto "DAA.Database.Migrations".</li>
	<li>2.- Pulsar la tecla "0" (la opción "0" es para insertar las semillas y la opción "1" es para vaciar tablas).</li>
	<li>3.- Escribís el nombre de la tabla que queréis añadir las semillas y pulsáis la tecla "Intro".</li>
</ul>
<p>En el caso de que queráis ejecutar todas las semillas tendréis que especificar el nombre de las tablas separado por comas. A continuación, dejo el valor que necesitáis introducir en el paso 3 para ejecutar todas las semillas que hay actualmente:</p>
<ul>
	<li><i>platforms, video_games, datatables_tables, datatables_records</i></li>
</ul>
<br />
