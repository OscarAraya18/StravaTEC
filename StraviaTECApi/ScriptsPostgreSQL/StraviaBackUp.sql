--
-- PostgreSQL database dump
--

-- Dumped from database version 13.0
-- Dumped by pg_dump version 13.0

-- Started on 2020-11-10 12:32:16

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 206 (class 1259 OID 42066)
-- Name: actividad; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.actividad (
    usuariodeportista character varying(20) NOT NULL,
    fechahora timestamp without time zone NOT NULL,
    nombre character varying(30),
    duracion time without time zone,
    kilometraje double precision,
    tipoactividad character varying(20),
    recorridogpx xml,
    nombreretocarrera character varying(30),
    adminretocarrera character varying(30),
    banderilla integer
);


ALTER TABLE public.actividad OWNER TO postgres;

--
-- TOC entry 208 (class 1259 OID 42081)
-- Name: amigo_deportista; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.amigo_deportista (
    usuariodeportista character varying(20) NOT NULL,
    amigoid character varying(20) NOT NULL
);


ALTER TABLE public.amigo_deportista OWNER TO postgres;

--
-- TOC entry 201 (class 1259 OID 42031)
-- Name: carrera; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.carrera (
    nombre character varying(30) NOT NULL,
    admindeportista character varying(20) NOT NULL,
    fecha date NOT NULL,
    recorrido xml,
    costo integer,
    tipoactividad character varying(30),
    privacidad boolean
);


ALTER TABLE public.carrera OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 42111)
-- Name: carrera_categoria; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.carrera_categoria (
    nombrecategoria character varying(20) NOT NULL,
    nombrecarrera character varying(30) NOT NULL,
    admindeportista character varying(20) NOT NULL
);


ALTER TABLE public.carrera_categoria OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 42121)
-- Name: carrera_cuentabancaria; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.carrera_cuentabancaria (
    nombrecarrera character varying(30) NOT NULL,
    admindeportista character varying(20) NOT NULL,
    cuentabancaria character varying(50) NOT NULL
);


ALTER TABLE public.carrera_cuentabancaria OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 42106)
-- Name: carrera_patrocinador; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.carrera_patrocinador (
    nombrepatrocinador character varying(30) NOT NULL,
    nombrecarrera character varying(30) NOT NULL,
    admindeportista character varying(20) NOT NULL
);


ALTER TABLE public.carrera_patrocinador OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 42061)
-- Name: categoria; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.categoria (
    nombre character varying(20) NOT NULL,
    descripcion character varying(150)
);


ALTER TABLE public.categoria OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 42023)
-- Name: deportista; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.deportista (
    usuario character varying(20) NOT NULL,
    claveacceso character varying(20) NOT NULL,
    fechanacimiento date,
    nombre character varying(20),
    apellido1 character varying(20),
    apellido2 character varying(20),
    nombrecategoria character varying(20),
    nacionalidad character varying(25),
    foto bytea
);


ALTER TABLE public.deportista OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 42126)
-- Name: deportista_carrera; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.deportista_carrera (
    usuariodeportista character varying(20) NOT NULL,
    nombrecarrera character varying(30) NOT NULL,
    admindeportista character varying(20) NOT NULL,
    completada boolean
);


ALTER TABLE public.deportista_carrera OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 42131)
-- Name: deportista_reto; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.deportista_reto (
    usuariodeportista character varying(20) NOT NULL,
    nombrereto character varying(30) NOT NULL,
    admindeportista character varying(20) NOT NULL,
    completado boolean,
    kmacumulados double precision
);


ALTER TABLE public.deportista_reto OWNER TO postgres;

--
-- TOC entry 207 (class 1259 OID 42074)
-- Name: grupo; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.grupo (
    nombre character varying(30) NOT NULL,
    admindeportista character varying(20) NOT NULL
);


ALTER TABLE public.grupo OWNER TO postgres;

--
-- TOC entry 210 (class 1259 OID 42091)
-- Name: grupo_carrera; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.grupo_carrera (
    nombrecarrera character varying(30) NOT NULL,
    admincarrera character varying(20) NOT NULL,
    admingrupo character varying(20) NOT NULL,
    nombregrupo character varying(30) NOT NULL
);


ALTER TABLE public.grupo_carrera OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 42086)
-- Name: grupo_deportista; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.grupo_deportista (
    usuariodeportista character varying(20) NOT NULL,
    nombregrupo character varying(30) NOT NULL,
    admindeportista character varying(20) NOT NULL
);


ALTER TABLE public.grupo_deportista OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 42096)
-- Name: grupo_reto; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.grupo_reto (
    nombrereto character varying(30) NOT NULL,
    adminreto character varying(20) NOT NULL,
    admingrupo character varying(20) NOT NULL,
    nombregrupo character varying(30) NOT NULL
);


ALTER TABLE public.grupo_reto OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 42053)
-- Name: inscripcion; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.inscripcion (
    usuariodeportista character varying(20) NOT NULL,
    estado character varying(10) NOT NULL,
    recibopago bytea
);


ALTER TABLE public.inscripcion OWNER TO postgres;

--
-- TOC entry 212 (class 1259 OID 42101)
-- Name: inscripcion_carrera; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.inscripcion_carrera (
    estadoinscripcion character varying(10) NOT NULL,
    deportistainscripcion character varying(20) NOT NULL,
    nombrecarrera character varying(30) NOT NULL,
    admincarrera character varying(20) NOT NULL
);


ALTER TABLE public.inscripcion_carrera OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 42048)
-- Name: patrocinador; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.patrocinador (
    nombrecomercial character varying(30) NOT NULL,
    logo character varying(200),
    nombrerepresentante character varying(100),
    numerotelrepresentante character varying(15)
);


ALTER TABLE public.patrocinador OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 42041)
-- Name: reto; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.reto (
    nombre character varying(30) NOT NULL,
    admindeportista character varying(20) NOT NULL,
    fondoaltitud character varying(7),
    tipoactividad character varying(20),
    periododisponibilidad date NOT NULL,
    privacidad boolean,
    kmtotales double precision,
    descripcion character varying(150)
);


ALTER TABLE public.reto OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 42116)
-- Name: reto_patrocinador; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.reto_patrocinador (
    nombrepatrocinador character varying(30) NOT NULL,
    nombrereto character varying(30) NOT NULL,
    admindeportista character varying(20) NOT NULL
);


ALTER TABLE public.reto_patrocinador OWNER TO postgres;

--
-- TOC entry 3131 (class 0 OID 42066)
-- Dependencies: 206
-- Data for Name: actividad; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.actividad (usuariodeportista, fechahora, nombre, duracion, kilometraje, tipoactividad, recorridogpx, nombreretocarrera, adminretocarrera, banderilla) FROM stdin;
auronplay	2020-10-11 14:00:00	Correr por la tarde	00:50:00	10	Ciclismo	\N	Endurance 2020	sam.astua	0
elpepe	2020-10-11 10:00:00	Correr por la mañana	01:10:00	10	Ciclismo	\N	Endurance 2020	sam.astua	0
\.


--
-- TOC entry 3133 (class 0 OID 42081)
-- Dependencies: 208
-- Data for Name: amigo_deportista; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.amigo_deportista (usuariodeportista, amigoid) FROM stdin;
sam.astua	kevintrox
sam.astua	auronplay
sam.astua	etesech
kevintrox	ozuna
kevintrox	cr7
kevintrox	ironman
crespo	cr7
crespo	cj
\.


--
-- TOC entry 3126 (class 0 OID 42031)
-- Dependencies: 201
-- Data for Name: carrera; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.carrera (nombre, admindeportista, fecha, recorrido, costo, tipoactividad, privacidad) FROM stdin;
Endurance 2020	sam.astua	2020-12-10	\N	15000	Ciclismo	t
The Best	cr7	2020-12-20	\N	40000	Correr	f
\.


--
-- TOC entry 3139 (class 0 OID 42111)
-- Dependencies: 214
-- Data for Name: carrera_categoria; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.carrera_categoria (nombrecategoria, nombrecarrera, admindeportista) FROM stdin;
Junior	Endurance 2020	sam.astua
Sub-23	Endurance 2020	sam.astua
Master A	Endurance 2020	sam.astua
Master B	Endurance 2020	sam.astua
Master C	Endurance 2020	sam.astua
Open	The Best	cr7
Sub-23	The Best	cr7
Master B	The Best	cr7
Master C	The Best	cr7
\.


--
-- TOC entry 3141 (class 0 OID 42121)
-- Dependencies: 216
-- Data for Name: carrera_cuentabancaria; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.carrera_cuentabancaria (nombrecarrera, admindeportista, cuentabancaria) FROM stdin;
Endurance 2020	sam.astua	CR05010056898889927823
Endurance 2020	sam.astua	CR05010053798887649823
The Best	cr7	CR05010053797417649855
The Best	cr7	CR05010053791947649822
\.


--
-- TOC entry 3138 (class 0 OID 42106)
-- Dependencies: 213
-- Data for Name: carrera_patrocinador; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.carrera_patrocinador (nombrepatrocinador, nombrecarrera, admindeportista) FROM stdin;
KOLBI	Endurance 2020	sam.astua
CoopeTarrazú	Endurance 2020	sam.astua
TDMAS	The Best	cr7
Grupo INS	The Best	cr7
\.


--
-- TOC entry 3130 (class 0 OID 42061)
-- Dependencies: 205
-- Data for Name: categoria; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.categoria (nombre, descripcion) FROM stdin;
Junior	menos de 15 años
Sub-23	de 15 a 23 años
Open	de 24 a 30 años
Elite	cualquiera que quiera inscribirse
Master A	de 30 a 40 años
Master B	de 41 a 50 años
Master C	más de 51 años
\.


--
-- TOC entry 3125 (class 0 OID 42023)
-- Dependencies: 200
-- Data for Name: deportista; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.deportista (usuario, claveacceso, fechanacimiento, nombre, apellido1, apellido2, nombrecategoria, nacionalidad, foto) FROM stdin;
sam.astua	password	2000-09-15	Saymon	Astúa	Madrigal	Sub-23	Costa Rica	\N
kevintrox	password	2000-12-21	Kevin	Acevedo	Rodríguez	Sub-23	Costa Rica	\N
etesech	password	1993-12-03	Sech	Morales	Williams	Open	Panamá	\N
elpepe	password	1964-01-24	Pepe	Ramírez	González	Master C	México	\N
crespo	password	1994-05-19	José	Crespo	Cepeda	Open	España	\N
ironman	password	1965-04-04	Robert	Downey	Jr	Master C	Estados Unidos	\N
ozuna	password	1974-06-10	Juan	Ozuna	Rosado	Master B	Puerto Rico	\N
cj	password	1978-07-11	Carl	Johnson	Rodríguez	Master B	Canadá	\N
auronplay	password	1988-11-05	Raúl	Álvarez	Genes	Master A	España	\N
cr7	password	1985-02-05	Cristiano	Dos Santos	Aveiro	Master A	Portugal	\N
\.


--
-- TOC entry 3142 (class 0 OID 42126)
-- Dependencies: 217
-- Data for Name: deportista_carrera; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.deportista_carrera (usuariodeportista, nombrecarrera, admindeportista, completada) FROM stdin;
elpepe	Endurance 2020	sam.astua	f
crespo	The Best	cr7	f
cj	The Best	cr7	f
auronplay	Endurance 2020	sam.astua	t
kevintrox	Endurance 2020	sam.astua	f
\.


--
-- TOC entry 3143 (class 0 OID 42131)
-- Dependencies: 218
-- Data for Name: deportista_reto; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.deportista_reto (usuariodeportista, nombrereto, admindeportista, completado, kmacumulados) FROM stdin;
etesech	Reto 1	kevintrox	f	0
cj	Reto 1	kevintrox	f	0
auronplay	Reto 2	sam.astua	f	0
ozuna	Reto 2	sam.astua	f	0
\.


--
-- TOC entry 3132 (class 0 OID 42074)
-- Dependencies: 207
-- Data for Name: grupo; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.grupo (nombre, admindeportista) FROM stdin;
Las Estrellas	sam.astua
Los Toros	kevintrox
Los Bichos	cr7
Los Físicos	crespo
\.


--
-- TOC entry 3135 (class 0 OID 42091)
-- Dependencies: 210
-- Data for Name: grupo_carrera; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.grupo_carrera (nombrecarrera, admincarrera, admingrupo, nombregrupo) FROM stdin;
Endurance 2020	sam.astua	sam.astua	Las Estrellas
Endurance 2020	sam.astua	kevintrox	Los Toros
\.


--
-- TOC entry 3134 (class 0 OID 42086)
-- Dependencies: 209
-- Data for Name: grupo_deportista; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.grupo_deportista (usuariodeportista, nombregrupo, admindeportista) FROM stdin;
etesech	Las Estrellas	sam.astua
elpepe	Las Estrellas	sam.astua
ozuna	Las Estrellas	sam.astua
sam.astua	Los Toros	kevintrox
auronplay	Los Toros	kevintrox
cj	Los Bichos	cr7
auronplay	Los Bichos	cr7
ironman	Los Físicos	crespo
cr7	Los Físicos	crespo
\.


--
-- TOC entry 3136 (class 0 OID 42096)
-- Dependencies: 211
-- Data for Name: grupo_reto; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.grupo_reto (nombrereto, adminreto, admingrupo, nombregrupo) FROM stdin;
Reto 2	sam.astua	cr7	Los Bichos
Reto 2	sam.astua	kevintrox	Los Toros
\.


--
-- TOC entry 3129 (class 0 OID 42053)
-- Dependencies: 204
-- Data for Name: inscripcion; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.inscripcion (usuariodeportista, estado, recibopago) FROM stdin;
auronplay	Aceptado	\N
elpepe	Aceptado	\N
crespo	Aceptado	\N
cj	Aceptado	\N
kevintrox	Aceptado	\N
\.


--
-- TOC entry 3137 (class 0 OID 42101)
-- Dependencies: 212
-- Data for Name: inscripcion_carrera; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.inscripcion_carrera (estadoinscripcion, deportistainscripcion, nombrecarrera, admincarrera) FROM stdin;
\.


--
-- TOC entry 3128 (class 0 OID 42048)
-- Dependencies: 203
-- Data for Name: patrocinador; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.patrocinador (nombrecomercial, logo, nombrerepresentante, numerotelrepresentante) FROM stdin;
Grupo INS	https://acortar.link/gGMhs	Róger Guillermo Arias Agüero	(+506)2287-6000
CoopeTarrazú	https://acortar.link/jtm5s	Yendry Leiva	(+506)2546-8615
KOLBI	https://acortar.link/rI9vm	Marjorie González Cascante	(+506)2255-1155
TDMAS	https://acortar.link/L2LyQ	Andres Nicolas	(+506)2232-2222
\.


--
-- TOC entry 3127 (class 0 OID 42041)
-- Dependencies: 202
-- Data for Name: reto; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.reto (nombre, admindeportista, fondoaltitud, tipoactividad, periododisponibilidad, privacidad, kmtotales, descripcion) FROM stdin;
Reto 1	kevintrox	fondo	Correr	2020-12-10	f	25	Deberá completar 25km corriendo
Reto 2	sam.astua	altitud	Ciclismo	2020-11-29	t	2	Deberá completar un total de 2km ascendidos en bicicleta
\.


--
-- TOC entry 3140 (class 0 OID 42116)
-- Dependencies: 215
-- Data for Name: reto_patrocinador; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.reto_patrocinador (nombrepatrocinador, nombrereto, admindeportista) FROM stdin;
KOLBI	Reto 1	kevintrox
TDMAS	Reto 1	kevintrox
CoopeTarrazú	Reto 2	sam.astua
Grupo INS	Reto 2	sam.astua
\.


--
-- TOC entry 2941 (class 2606 OID 42073)
-- Name: actividad actividad_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.actividad
    ADD CONSTRAINT actividad_pkey PRIMARY KEY (usuariodeportista, fechahora);


--
-- TOC entry 2947 (class 2606 OID 42085)
-- Name: amigo_deportista amigo_deportista_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.amigo_deportista
    ADD CONSTRAINT amigo_deportista_pkey PRIMARY KEY (usuariodeportista, amigoid);


--
-- TOC entry 2959 (class 2606 OID 42115)
-- Name: carrera_categoria carrera_categoria_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.carrera_categoria
    ADD CONSTRAINT carrera_categoria_pkey PRIMARY KEY (nombrecategoria, nombrecarrera, admindeportista);


--
-- TOC entry 2963 (class 2606 OID 42125)
-- Name: carrera_cuentabancaria carrera_cuentabancaria_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.carrera_cuentabancaria
    ADD CONSTRAINT carrera_cuentabancaria_pkey PRIMARY KEY (nombrecarrera, admindeportista, cuentabancaria);


--
-- TOC entry 2927 (class 2606 OID 42040)
-- Name: carrera carrera_nombre_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.carrera
    ADD CONSTRAINT carrera_nombre_key UNIQUE (nombre);


--
-- TOC entry 2957 (class 2606 OID 42110)
-- Name: carrera_patrocinador carrera_patrocinador_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.carrera_patrocinador
    ADD CONSTRAINT carrera_patrocinador_pkey PRIMARY KEY (nombrepatrocinador, nombrecarrera, admindeportista);


--
-- TOC entry 2929 (class 2606 OID 42038)
-- Name: carrera carrera_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.carrera
    ADD CONSTRAINT carrera_pkey PRIMARY KEY (nombre, admindeportista);


--
-- TOC entry 2939 (class 2606 OID 42065)
-- Name: categoria categoria_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categoria
    ADD CONSTRAINT categoria_pkey PRIMARY KEY (nombre);


--
-- TOC entry 2965 (class 2606 OID 42130)
-- Name: deportista_carrera deportista_carrera_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deportista_carrera
    ADD CONSTRAINT deportista_carrera_pkey PRIMARY KEY (usuariodeportista, nombrecarrera, admindeportista);


--
-- TOC entry 2925 (class 2606 OID 42030)
-- Name: deportista deportista_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deportista
    ADD CONSTRAINT deportista_pkey PRIMARY KEY (usuario);


--
-- TOC entry 2967 (class 2606 OID 42135)
-- Name: deportista_reto deportista_reto_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deportista_reto
    ADD CONSTRAINT deportista_reto_pkey PRIMARY KEY (usuariodeportista, nombrereto, admindeportista);


--
-- TOC entry 2951 (class 2606 OID 42095)
-- Name: grupo_carrera grupo_carrera_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupo_carrera
    ADD CONSTRAINT grupo_carrera_pkey PRIMARY KEY (nombrecarrera, admincarrera, admingrupo, nombregrupo);


--
-- TOC entry 2949 (class 2606 OID 42090)
-- Name: grupo_deportista grupo_deportista_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupo_deportista
    ADD CONSTRAINT grupo_deportista_pkey PRIMARY KEY (usuariodeportista, nombregrupo, admindeportista);


--
-- TOC entry 2943 (class 2606 OID 42080)
-- Name: grupo grupo_nombre_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupo
    ADD CONSTRAINT grupo_nombre_key UNIQUE (nombre);


--
-- TOC entry 2945 (class 2606 OID 42078)
-- Name: grupo grupo_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupo
    ADD CONSTRAINT grupo_pkey PRIMARY KEY (nombre, admindeportista);


--
-- TOC entry 2953 (class 2606 OID 42100)
-- Name: grupo_reto grupo_reto_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupo_reto
    ADD CONSTRAINT grupo_reto_pkey PRIMARY KEY (nombrereto, adminreto, admingrupo, nombregrupo);


--
-- TOC entry 2955 (class 2606 OID 42105)
-- Name: inscripcion_carrera inscripcion_carrera_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inscripcion_carrera
    ADD CONSTRAINT inscripcion_carrera_pkey PRIMARY KEY (estadoinscripcion, nombrecarrera, deportistainscripcion, admincarrera);


--
-- TOC entry 2937 (class 2606 OID 42060)
-- Name: inscripcion inscripcion_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inscripcion
    ADD CONSTRAINT inscripcion_pkey PRIMARY KEY (estado, usuariodeportista);


--
-- TOC entry 2935 (class 2606 OID 42052)
-- Name: patrocinador patrocinador_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.patrocinador
    ADD CONSTRAINT patrocinador_pkey PRIMARY KEY (nombrecomercial);


--
-- TOC entry 2931 (class 2606 OID 42047)
-- Name: reto reto_nombre_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reto
    ADD CONSTRAINT reto_nombre_key UNIQUE (nombre);


--
-- TOC entry 2961 (class 2606 OID 42120)
-- Name: reto_patrocinador reto_patrocinador_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reto_patrocinador
    ADD CONSTRAINT reto_patrocinador_pkey PRIMARY KEY (nombrepatrocinador, nombrereto, admindeportista);


--
-- TOC entry 2933 (class 2606 OID 42045)
-- Name: reto reto_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reto
    ADD CONSTRAINT reto_pkey PRIMARY KEY (nombre, admindeportista);


--
-- TOC entry 2972 (class 2606 OID 42146)
-- Name: actividad actividad_usuariodeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.actividad
    ADD CONSTRAINT actividad_usuariodeportista_fkey FOREIGN KEY (usuariodeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;


--
-- TOC entry 2975 (class 2606 OID 42246)
-- Name: amigo_deportista amigo_deportista_amigoid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.amigo_deportista
    ADD CONSTRAINT amigo_deportista_amigoid_fkey FOREIGN KEY (amigoid) REFERENCES public.deportista(usuario) ON DELETE CASCADE;


--
-- TOC entry 2974 (class 2606 OID 42241)
-- Name: amigo_deportista amigo_deportista_usuariodeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.amigo_deportista
    ADD CONSTRAINT amigo_deportista_usuariodeportista_fkey FOREIGN KEY (usuariodeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;


--
-- TOC entry 2969 (class 2606 OID 42161)
-- Name: carrera carrera_admindeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.carrera
    ADD CONSTRAINT carrera_admindeportista_fkey FOREIGN KEY (admindeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;


--
-- TOC entry 2986 (class 2606 OID 42201)
-- Name: carrera_categoria carrera_categoria_nombrecarrera_admindeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.carrera_categoria
    ADD CONSTRAINT carrera_categoria_nombrecarrera_admindeportista_fkey FOREIGN KEY (nombrecarrera, admindeportista) REFERENCES public.carrera(nombre, admindeportista) ON DELETE CASCADE;


--
-- TOC entry 2987 (class 2606 OID 42206)
-- Name: carrera_categoria carrera_categoria_nombrecategoria_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.carrera_categoria
    ADD CONSTRAINT carrera_categoria_nombrecategoria_fkey FOREIGN KEY (nombrecategoria) REFERENCES public.categoria(nombre) ON DELETE CASCADE;


--
-- TOC entry 2990 (class 2606 OID 42166)
-- Name: carrera_cuentabancaria carrera_cuentabancaria_nombrecarrera_admindeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.carrera_cuentabancaria
    ADD CONSTRAINT carrera_cuentabancaria_nombrecarrera_admindeportista_fkey FOREIGN KEY (nombrecarrera, admindeportista) REFERENCES public.carrera(nombre, admindeportista) ON DELETE CASCADE;


--
-- TOC entry 2985 (class 2606 OID 42216)
-- Name: carrera_patrocinador carrera_patrocinador_nombrecarrera_admindeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.carrera_patrocinador
    ADD CONSTRAINT carrera_patrocinador_nombrecarrera_admindeportista_fkey FOREIGN KEY (nombrecarrera, admindeportista) REFERENCES public.carrera(nombre, admindeportista) ON DELETE CASCADE;


--
-- TOC entry 2984 (class 2606 OID 42211)
-- Name: carrera_patrocinador carrera_patrocinador_nombrepatrocinador_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.carrera_patrocinador
    ADD CONSTRAINT carrera_patrocinador_nombrepatrocinador_fkey FOREIGN KEY (nombrepatrocinador) REFERENCES public.patrocinador(nombrecomercial) ON DELETE CASCADE;


--
-- TOC entry 2992 (class 2606 OID 42266)
-- Name: deportista_carrera deportista_carrera_nombrecarrera_admindeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deportista_carrera
    ADD CONSTRAINT deportista_carrera_nombrecarrera_admindeportista_fkey FOREIGN KEY (nombrecarrera, admindeportista) REFERENCES public.carrera(nombre, admindeportista) ON DELETE CASCADE;


--
-- TOC entry 2991 (class 2606 OID 42261)
-- Name: deportista_carrera deportista_carrera_usuariodeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deportista_carrera
    ADD CONSTRAINT deportista_carrera_usuariodeportista_fkey FOREIGN KEY (usuariodeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;


--
-- TOC entry 2968 (class 2606 OID 42156)
-- Name: deportista deportista_nombrecategoria_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deportista
    ADD CONSTRAINT deportista_nombrecategoria_fkey FOREIGN KEY (nombrecategoria) REFERENCES public.categoria(nombre) ON DELETE CASCADE;


--
-- TOC entry 2994 (class 2606 OID 42256)
-- Name: deportista_reto deportista_reto_nombrereto_admindeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deportista_reto
    ADD CONSTRAINT deportista_reto_nombrereto_admindeportista_fkey FOREIGN KEY (nombrereto, admindeportista) REFERENCES public.reto(nombre, admindeportista) ON DELETE CASCADE;


--
-- TOC entry 2993 (class 2606 OID 42251)
-- Name: deportista_reto deportista_reto_usuariodeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.deportista_reto
    ADD CONSTRAINT deportista_reto_usuariodeportista_fkey FOREIGN KEY (usuariodeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;


--
-- TOC entry 2973 (class 2606 OID 42151)
-- Name: grupo grupo_admindeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupo
    ADD CONSTRAINT grupo_admindeportista_fkey FOREIGN KEY (admindeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;


--
-- TOC entry 2979 (class 2606 OID 42186)
-- Name: grupo_carrera grupo_carrera_nombrecarrera_admincarrera_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupo_carrera
    ADD CONSTRAINT grupo_carrera_nombrecarrera_admincarrera_fkey FOREIGN KEY (nombrecarrera, admincarrera) REFERENCES public.carrera(nombre, admindeportista) ON DELETE CASCADE;


--
-- TOC entry 2978 (class 2606 OID 42181)
-- Name: grupo_carrera grupo_carrera_nombregrupo_admingrupo_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupo_carrera
    ADD CONSTRAINT grupo_carrera_nombregrupo_admingrupo_fkey FOREIGN KEY (nombregrupo, admingrupo) REFERENCES public.grupo(nombre, admindeportista) ON DELETE CASCADE;


--
-- TOC entry 2977 (class 2606 OID 42176)
-- Name: grupo_deportista grupo_deportista_nombregrupo_admindeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupo_deportista
    ADD CONSTRAINT grupo_deportista_nombregrupo_admindeportista_fkey FOREIGN KEY (nombregrupo, admindeportista) REFERENCES public.grupo(nombre, admindeportista) ON DELETE CASCADE;


--
-- TOC entry 2976 (class 2606 OID 42171)
-- Name: grupo_deportista grupo_deportista_usuariodeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupo_deportista
    ADD CONSTRAINT grupo_deportista_usuariodeportista_fkey FOREIGN KEY (usuariodeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;


--
-- TOC entry 2980 (class 2606 OID 42191)
-- Name: grupo_reto grupo_reto_nombregrupo_admingrupo_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupo_reto
    ADD CONSTRAINT grupo_reto_nombregrupo_admingrupo_fkey FOREIGN KEY (nombregrupo, admingrupo) REFERENCES public.grupo(nombre, admindeportista) ON DELETE CASCADE;


--
-- TOC entry 2981 (class 2606 OID 42196)
-- Name: grupo_reto grupo_reto_nombrereto_adminreto_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.grupo_reto
    ADD CONSTRAINT grupo_reto_nombrereto_adminreto_fkey FOREIGN KEY (nombrereto, adminreto) REFERENCES public.reto(nombre, admindeportista) ON DELETE CASCADE;


--
-- TOC entry 2982 (class 2606 OID 42231)
-- Name: inscripcion_carrera inscripcion_carrera_estadoinscripcion_deportistainscripcio_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inscripcion_carrera
    ADD CONSTRAINT inscripcion_carrera_estadoinscripcion_deportistainscripcio_fkey FOREIGN KEY (estadoinscripcion, deportistainscripcion) REFERENCES public.inscripcion(estado, usuariodeportista) ON DELETE CASCADE;


--
-- TOC entry 2983 (class 2606 OID 42236)
-- Name: inscripcion_carrera inscripcion_carrera_nombrecarrera_admincarrera_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inscripcion_carrera
    ADD CONSTRAINT inscripcion_carrera_nombrecarrera_admincarrera_fkey FOREIGN KEY (nombrecarrera, admincarrera) REFERENCES public.carrera(nombre, admindeportista) ON DELETE CASCADE;


--
-- TOC entry 2971 (class 2606 OID 42141)
-- Name: inscripcion inscripcion_usuariodeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inscripcion
    ADD CONSTRAINT inscripcion_usuariodeportista_fkey FOREIGN KEY (usuariodeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;


--
-- TOC entry 2970 (class 2606 OID 42136)
-- Name: reto reto_admindeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reto
    ADD CONSTRAINT reto_admindeportista_fkey FOREIGN KEY (admindeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;


--
-- TOC entry 2988 (class 2606 OID 42221)
-- Name: reto_patrocinador reto_patrocinador_nombrepatrocinador_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reto_patrocinador
    ADD CONSTRAINT reto_patrocinador_nombrepatrocinador_fkey FOREIGN KEY (nombrepatrocinador) REFERENCES public.patrocinador(nombrecomercial) ON DELETE CASCADE;


--
-- TOC entry 2989 (class 2606 OID 42226)
-- Name: reto_patrocinador reto_patrocinador_nombrereto_admindeportista_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reto_patrocinador
    ADD CONSTRAINT reto_patrocinador_nombrereto_admindeportista_fkey FOREIGN KEY (nombrereto, admindeportista) REFERENCES public.reto(nombre, admindeportista) ON DELETE CASCADE;


-- Completed on 2020-11-10 12:32:16

--
-- PostgreSQL database dump complete
--

