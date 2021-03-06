PGDMP                     
    x         	   StraviaDB    13.0    13.0 Z    D           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            E           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            F           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            G           1262    20902 	   StraviaDB    DATABASE     l   CREATE DATABASE "StraviaDB" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Spanish_Costa Rica.1252';
    DROP DATABASE "StraviaDB";
                StraviaTECAdmin    false            �            1259    57187 	   actividad    TABLE     �  CREATE TABLE public.actividad (
    usuariodeportista character varying(20) NOT NULL,
    fechahora timestamp without time zone NOT NULL,
    nombre character varying(100),
    duracion time without time zone NOT NULL,
    kilometraje double precision NOT NULL,
    tipoactividad character varying(20),
    recorridogpx xml,
    nombreretocarrera character varying(100),
    adminretocarrera character varying(30),
    banderilla integer
);
    DROP TABLE public.actividad;
       public         heap    postgres    false            �            1259    57203    amigo_deportista    TABLE     �   CREATE TABLE public.amigo_deportista (
    usuariodeportista character varying(20) NOT NULL,
    amigoid character varying(20) NOT NULL
);
 $   DROP TABLE public.amigo_deportista;
       public         heap    postgres    false            �            1259    57152    carrera    TABLE     	  CREATE TABLE public.carrera (
    nombre character varying(100) NOT NULL,
    admindeportista character varying(20) NOT NULL,
    fecha date NOT NULL,
    recorrido xml,
    costo integer NOT NULL,
    tipoactividad character varying(30),
    privacidad boolean
);
    DROP TABLE public.carrera;
       public         heap    postgres    false            �            1259    57228    carrera_categoria    TABLE     �   CREATE TABLE public.carrera_categoria (
    nombrecategoria character varying(20) NOT NULL,
    nombrecarrera character varying(100) NOT NULL,
    admindeportista character varying(20) NOT NULL
);
 %   DROP TABLE public.carrera_categoria;
       public         heap    postgres    false            �            1259    57238    carrera_cuentabancaria    TABLE     �   CREATE TABLE public.carrera_cuentabancaria (
    nombrecarrera character varying(100) NOT NULL,
    admindeportista character varying(20) NOT NULL,
    cuentabancaria character varying(50) NOT NULL
);
 *   DROP TABLE public.carrera_cuentabancaria;
       public         heap    postgres    false            �            1259    57223    carrera_patrocinador    TABLE     �   CREATE TABLE public.carrera_patrocinador (
    nombrepatrocinador character varying(30) NOT NULL,
    nombrecarrera character varying(100) NOT NULL,
    admindeportista character varying(20) NOT NULL
);
 (   DROP TABLE public.carrera_patrocinador;
       public         heap    postgres    false            �            1259    57182 	   categoria    TABLE     u   CREATE TABLE public.categoria (
    nombre character varying(20) NOT NULL,
    descripcion character varying(150)
);
    DROP TABLE public.categoria;
       public         heap    postgres    false            �            1259    57144 
   deportista    TABLE     h  CREATE TABLE public.deportista (
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
    DROP TABLE public.deportista;
       public         heap    postgres    false            �            1259    57243    deportista_carrera    TABLE     �   CREATE TABLE public.deportista_carrera (
    usuariodeportista character varying(20) NOT NULL,
    nombrecarrera character varying(100) NOT NULL,
    admindeportista character varying(20) NOT NULL,
    completada boolean
);
 &   DROP TABLE public.deportista_carrera;
       public         heap    postgres    false            �            1259    57248    deportista_reto    TABLE     �   CREATE TABLE public.deportista_reto (
    usuariodeportista character varying(20) NOT NULL,
    nombrereto character varying(100) NOT NULL,
    admindeportista character varying(20) NOT NULL,
    completado boolean,
    kmacumulados double precision
);
 #   DROP TABLE public.deportista_reto;
       public         heap    postgres    false            �            1259    57197    grupo    TABLE     �   CREATE TABLE public.grupo (
    id integer NOT NULL,
    nombre character varying(30) NOT NULL,
    admindeportista character varying(20) NOT NULL
);
    DROP TABLE public.grupo;
       public         heap    postgres    false            �            1259    57213    grupo_carrera    TABLE     �   CREATE TABLE public.grupo_carrera (
    nombrecarrera character varying(100) NOT NULL,
    admincarrera character varying(20) NOT NULL,
    admingrupo character varying(20) NOT NULL,
    idgrupo integer NOT NULL
);
 !   DROP TABLE public.grupo_carrera;
       public         heap    postgres    false            �            1259    57208    grupo_deportista    TABLE     �   CREATE TABLE public.grupo_deportista (
    usuariodeportista character varying(20) NOT NULL,
    idgrupo integer NOT NULL,
    admindeportista character varying(20) NOT NULL
);
 $   DROP TABLE public.grupo_deportista;
       public         heap    postgres    false            �            1259    57195    grupo_id_seq    SEQUENCE     �   CREATE SEQUENCE public.grupo_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.grupo_id_seq;
       public          postgres    false    208            H           0    0    grupo_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.grupo_id_seq OWNED BY public.grupo.id;
          public          postgres    false    207            �            1259    57218 
   grupo_reto    TABLE     �   CREATE TABLE public.grupo_reto (
    nombrereto character varying(100) NOT NULL,
    adminreto character varying(20) NOT NULL,
    admingrupo character varying(20) NOT NULL,
    idgrupo integer NOT NULL
);
    DROP TABLE public.grupo_reto;
       public         heap    postgres    false            �            1259    57174    inscripcion    TABLE     �   CREATE TABLE public.inscripcion (
    usuariodeportista character varying(20) NOT NULL,
    estado character varying(10),
    recibopago bytea,
    nombrecarrera character varying(100) NOT NULL,
    admincarrera character varying(20) NOT NULL
);
    DROP TABLE public.inscripcion;
       public         heap    postgres    false            �            1259    57169    patrocinador    TABLE     �   CREATE TABLE public.patrocinador (
    nombrecomercial character varying(50) NOT NULL,
    logo character varying(200),
    nombrerepresentante character varying(100),
    numerotelrepresentante character varying(15)
);
     DROP TABLE public.patrocinador;
       public         heap    postgres    false            �            1259    57162    reto    TABLE     _  CREATE TABLE public.reto (
    nombre character varying(100) NOT NULL,
    admindeportista character varying(20) NOT NULL,
    fondoaltitud character varying(7),
    tipoactividad character varying(20),
    periododisponibilidad date NOT NULL,
    privacidad boolean,
    kmtotales double precision NOT NULL,
    descripcion character varying(150)
);
    DROP TABLE public.reto;
       public         heap    postgres    false            �            1259    57233    reto_patrocinador    TABLE     �   CREATE TABLE public.reto_patrocinador (
    nombrepatrocinador character varying(30) NOT NULL,
    nombrereto character varying(100) NOT NULL,
    admindeportista character varying(20) NOT NULL
);
 %   DROP TABLE public.reto_patrocinador;
       public         heap    postgres    false            j           2604    57200    grupo id    DEFAULT     d   ALTER TABLE ONLY public.grupo ALTER COLUMN id SET DEFAULT nextval('public.grupo_id_seq'::regclass);
 7   ALTER TABLE public.grupo ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    208    207    208            5          0    57187 	   actividad 
   TABLE DATA           �   COPY public.actividad (usuariodeportista, fechahora, nombre, duracion, kilometraje, tipoactividad, recorridogpx, nombreretocarrera, adminretocarrera, banderilla) FROM stdin;
    public          postgres    false    206   ��       8          0    57203    amigo_deportista 
   TABLE DATA           F   COPY public.amigo_deportista (usuariodeportista, amigoid) FROM stdin;
    public          postgres    false    209   �       0          0    57152    carrera 
   TABLE DATA           n   COPY public.carrera (nombre, admindeportista, fecha, recorrido, costo, tipoactividad, privacidad) FROM stdin;
    public          postgres    false    201   j�       =          0    57228    carrera_categoria 
   TABLE DATA           \   COPY public.carrera_categoria (nombrecategoria, nombrecarrera, admindeportista) FROM stdin;
    public          postgres    false    214   ه       ?          0    57238    carrera_cuentabancaria 
   TABLE DATA           `   COPY public.carrera_cuentabancaria (nombrecarrera, admindeportista, cuentabancaria) FROM stdin;
    public          postgres    false    216   J�       <          0    57223    carrera_patrocinador 
   TABLE DATA           b   COPY public.carrera_patrocinador (nombrepatrocinador, nombrecarrera, admindeportista) FROM stdin;
    public          postgres    false    213   ��       4          0    57182 	   categoria 
   TABLE DATA           8   COPY public.categoria (nombre, descripcion) FROM stdin;
    public          postgres    false    205   )�       /          0    57144 
   deportista 
   TABLE DATA           �   COPY public.deportista (usuario, claveacceso, fechanacimiento, nombre, apellido1, apellido2, nombrecategoria, nacionalidad, foto) FROM stdin;
    public          postgres    false    200   ĉ       @          0    57243    deportista_carrera 
   TABLE DATA           k   COPY public.deportista_carrera (usuariodeportista, nombrecarrera, admindeportista, completada) FROM stdin;
    public          postgres    false    217   ŧ       A          0    57248    deportista_reto 
   TABLE DATA           s   COPY public.deportista_reto (usuariodeportista, nombrereto, admindeportista, completado, kmacumulados) FROM stdin;
    public          postgres    false    218   a�       7          0    57197    grupo 
   TABLE DATA           <   COPY public.grupo (id, nombre, admindeportista) FROM stdin;
    public          postgres    false    208   ��       :          0    57213    grupo_carrera 
   TABLE DATA           Y   COPY public.grupo_carrera (nombrecarrera, admincarrera, admingrupo, idgrupo) FROM stdin;
    public          postgres    false    211   '�       9          0    57208    grupo_deportista 
   TABLE DATA           W   COPY public.grupo_deportista (usuariodeportista, idgrupo, admindeportista) FROM stdin;
    public          postgres    false    210   o�       ;          0    57218 
   grupo_reto 
   TABLE DATA           P   COPY public.grupo_reto (nombrereto, adminreto, admingrupo, idgrupo) FROM stdin;
    public          postgres    false    212   �       3          0    57174    inscripcion 
   TABLE DATA           i   COPY public.inscripcion (usuariodeportista, estado, recibopago, nombrecarrera, admincarrera) FROM stdin;
    public          postgres    false    204   $�       2          0    57169    patrocinador 
   TABLE DATA           j   COPY public.patrocinador (nombrecomercial, logo, nombrerepresentante, numerotelrepresentante) FROM stdin;
    public          postgres    false    203   ۪       1          0    57162    reto 
   TABLE DATA           �   COPY public.reto (nombre, admindeportista, fondoaltitud, tipoactividad, periododisponibilidad, privacidad, kmtotales, descripcion) FROM stdin;
    public          postgres    false    202   ū       >          0    57233    reto_patrocinador 
   TABLE DATA           \   COPY public.reto_patrocinador (nombrepatrocinador, nombrereto, admindeportista) FROM stdin;
    public          postgres    false    215   l�       I           0    0    grupo_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.grupo_id_seq', 1, false);
          public          postgres    false    207            |           2606    57194    actividad actividad_pkey 
   CONSTRAINT     p   ALTER TABLE ONLY public.actividad
    ADD CONSTRAINT actividad_pkey PRIMARY KEY (usuariodeportista, fechahora);
 B   ALTER TABLE ONLY public.actividad DROP CONSTRAINT actividad_pkey;
       public            postgres    false    206    206            �           2606    57207 &   amigo_deportista amigo_deportista_pkey 
   CONSTRAINT     |   ALTER TABLE ONLY public.amigo_deportista
    ADD CONSTRAINT amigo_deportista_pkey PRIMARY KEY (usuariodeportista, amigoid);
 P   ALTER TABLE ONLY public.amigo_deportista DROP CONSTRAINT amigo_deportista_pkey;
       public            postgres    false    209    209            �           2606    57232 (   carrera_categoria carrera_categoria_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.carrera_categoria
    ADD CONSTRAINT carrera_categoria_pkey PRIMARY KEY (nombrecategoria, nombrecarrera, admindeportista);
 R   ALTER TABLE ONLY public.carrera_categoria DROP CONSTRAINT carrera_categoria_pkey;
       public            postgres    false    214    214    214            �           2606    57242 2   carrera_cuentabancaria carrera_cuentabancaria_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.carrera_cuentabancaria
    ADD CONSTRAINT carrera_cuentabancaria_pkey PRIMARY KEY (nombrecarrera, admindeportista, cuentabancaria);
 \   ALTER TABLE ONLY public.carrera_cuentabancaria DROP CONSTRAINT carrera_cuentabancaria_pkey;
       public            postgres    false    216    216    216            n           2606    57161    carrera carrera_nombre_key 
   CONSTRAINT     W   ALTER TABLE ONLY public.carrera
    ADD CONSTRAINT carrera_nombre_key UNIQUE (nombre);
 D   ALTER TABLE ONLY public.carrera DROP CONSTRAINT carrera_nombre_key;
       public            postgres    false    201            �           2606    57227 .   carrera_patrocinador carrera_patrocinador_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.carrera_patrocinador
    ADD CONSTRAINT carrera_patrocinador_pkey PRIMARY KEY (nombrepatrocinador, nombrecarrera, admindeportista);
 X   ALTER TABLE ONLY public.carrera_patrocinador DROP CONSTRAINT carrera_patrocinador_pkey;
       public            postgres    false    213    213    213            p           2606    57159    carrera carrera_pkey 
   CONSTRAINT     g   ALTER TABLE ONLY public.carrera
    ADD CONSTRAINT carrera_pkey PRIMARY KEY (nombre, admindeportista);
 >   ALTER TABLE ONLY public.carrera DROP CONSTRAINT carrera_pkey;
       public            postgres    false    201    201            z           2606    57186    categoria categoria_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.categoria
    ADD CONSTRAINT categoria_pkey PRIMARY KEY (nombre);
 B   ALTER TABLE ONLY public.categoria DROP CONSTRAINT categoria_pkey;
       public            postgres    false    205            �           2606    57247 *   deportista_carrera deportista_carrera_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.deportista_carrera
    ADD CONSTRAINT deportista_carrera_pkey PRIMARY KEY (usuariodeportista, nombrecarrera, admindeportista);
 T   ALTER TABLE ONLY public.deportista_carrera DROP CONSTRAINT deportista_carrera_pkey;
       public            postgres    false    217    217    217            l           2606    57151    deportista deportista_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public.deportista
    ADD CONSTRAINT deportista_pkey PRIMARY KEY (usuario);
 D   ALTER TABLE ONLY public.deportista DROP CONSTRAINT deportista_pkey;
       public            postgres    false    200            �           2606    57252 $   deportista_reto deportista_reto_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.deportista_reto
    ADD CONSTRAINT deportista_reto_pkey PRIMARY KEY (usuariodeportista, nombrereto, admindeportista);
 N   ALTER TABLE ONLY public.deportista_reto DROP CONSTRAINT deportista_reto_pkey;
       public            postgres    false    218    218    218            �           2606    57217     grupo_carrera grupo_carrera_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.grupo_carrera
    ADD CONSTRAINT grupo_carrera_pkey PRIMARY KEY (nombrecarrera, admincarrera, admingrupo, idgrupo);
 J   ALTER TABLE ONLY public.grupo_carrera DROP CONSTRAINT grupo_carrera_pkey;
       public            postgres    false    211    211    211    211            �           2606    57212 &   grupo_deportista grupo_deportista_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.grupo_deportista
    ADD CONSTRAINT grupo_deportista_pkey PRIMARY KEY (usuariodeportista, idgrupo, admindeportista);
 P   ALTER TABLE ONLY public.grupo_deportista DROP CONSTRAINT grupo_deportista_pkey;
       public            postgres    false    210    210    210            ~           2606    57202    grupo grupo_pkey 
   CONSTRAINT     _   ALTER TABLE ONLY public.grupo
    ADD CONSTRAINT grupo_pkey PRIMARY KEY (id, admindeportista);
 :   ALTER TABLE ONLY public.grupo DROP CONSTRAINT grupo_pkey;
       public            postgres    false    208    208            �           2606    57222    grupo_reto grupo_reto_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.grupo_reto
    ADD CONSTRAINT grupo_reto_pkey PRIMARY KEY (nombrereto, adminreto, admingrupo, idgrupo);
 D   ALTER TABLE ONLY public.grupo_reto DROP CONSTRAINT grupo_reto_pkey;
       public            postgres    false    212    212    212    212            x           2606    57181    inscripcion inscripcion_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.inscripcion
    ADD CONSTRAINT inscripcion_pkey PRIMARY KEY (usuariodeportista, nombrecarrera, admincarrera);
 F   ALTER TABLE ONLY public.inscripcion DROP CONSTRAINT inscripcion_pkey;
       public            postgres    false    204    204    204            v           2606    57173    patrocinador patrocinador_pkey 
   CONSTRAINT     i   ALTER TABLE ONLY public.patrocinador
    ADD CONSTRAINT patrocinador_pkey PRIMARY KEY (nombrecomercial);
 H   ALTER TABLE ONLY public.patrocinador DROP CONSTRAINT patrocinador_pkey;
       public            postgres    false    203            r           2606    57168    reto reto_nombre_key 
   CONSTRAINT     Q   ALTER TABLE ONLY public.reto
    ADD CONSTRAINT reto_nombre_key UNIQUE (nombre);
 >   ALTER TABLE ONLY public.reto DROP CONSTRAINT reto_nombre_key;
       public            postgres    false    202            �           2606    57237 (   reto_patrocinador reto_patrocinador_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public.reto_patrocinador
    ADD CONSTRAINT reto_patrocinador_pkey PRIMARY KEY (nombrepatrocinador, nombrereto, admindeportista);
 R   ALTER TABLE ONLY public.reto_patrocinador DROP CONSTRAINT reto_patrocinador_pkey;
       public            postgres    false    215    215    215            t           2606    57166    reto reto_pkey 
   CONSTRAINT     a   ALTER TABLE ONLY public.reto
    ADD CONSTRAINT reto_pkey PRIMARY KEY (nombre, admindeportista);
 8   ALTER TABLE ONLY public.reto DROP CONSTRAINT reto_pkey;
       public            postgres    false    202    202            �           2606    57268 *   actividad actividad_usuariodeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.actividad
    ADD CONSTRAINT actividad_usuariodeportista_fkey FOREIGN KEY (usuariodeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;
 T   ALTER TABLE ONLY public.actividad DROP CONSTRAINT actividad_usuariodeportista_fkey;
       public          postgres    false    200    2924    206            �           2606    57358 .   amigo_deportista amigo_deportista_amigoid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.amigo_deportista
    ADD CONSTRAINT amigo_deportista_amigoid_fkey FOREIGN KEY (amigoid) REFERENCES public.deportista(usuario) ON DELETE CASCADE;
 X   ALTER TABLE ONLY public.amigo_deportista DROP CONSTRAINT amigo_deportista_amigoid_fkey;
       public          postgres    false    2924    200    209            �           2606    57353 8   amigo_deportista amigo_deportista_usuariodeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.amigo_deportista
    ADD CONSTRAINT amigo_deportista_usuariodeportista_fkey FOREIGN KEY (usuariodeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;
 b   ALTER TABLE ONLY public.amigo_deportista DROP CONSTRAINT amigo_deportista_usuariodeportista_fkey;
       public          postgres    false    200    2924    209            �           2606    57283 $   carrera carrera_admindeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.carrera
    ADD CONSTRAINT carrera_admindeportista_fkey FOREIGN KEY (admindeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;
 N   ALTER TABLE ONLY public.carrera DROP CONSTRAINT carrera_admindeportista_fkey;
       public          postgres    false    200    201    2924            �           2606    57323 F   carrera_categoria carrera_categoria_nombrecarrera_admindeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.carrera_categoria
    ADD CONSTRAINT carrera_categoria_nombrecarrera_admindeportista_fkey FOREIGN KEY (nombrecarrera, admindeportista) REFERENCES public.carrera(nombre, admindeportista) ON DELETE CASCADE;
 p   ALTER TABLE ONLY public.carrera_categoria DROP CONSTRAINT carrera_categoria_nombrecarrera_admindeportista_fkey;
       public          postgres    false    214    201    201    2928    214            �           2606    57328 8   carrera_categoria carrera_categoria_nombrecategoria_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.carrera_categoria
    ADD CONSTRAINT carrera_categoria_nombrecategoria_fkey FOREIGN KEY (nombrecategoria) REFERENCES public.categoria(nombre) ON DELETE CASCADE;
 b   ALTER TABLE ONLY public.carrera_categoria DROP CONSTRAINT carrera_categoria_nombrecategoria_fkey;
       public          postgres    false    2938    214    205            �           2606    57288 P   carrera_cuentabancaria carrera_cuentabancaria_nombrecarrera_admindeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.carrera_cuentabancaria
    ADD CONSTRAINT carrera_cuentabancaria_nombrecarrera_admindeportista_fkey FOREIGN KEY (nombrecarrera, admindeportista) REFERENCES public.carrera(nombre, admindeportista) ON DELETE CASCADE;
 z   ALTER TABLE ONLY public.carrera_cuentabancaria DROP CONSTRAINT carrera_cuentabancaria_nombrecarrera_admindeportista_fkey;
       public          postgres    false    216    201    201    2928    216            �           2606    57338 L   carrera_patrocinador carrera_patrocinador_nombrecarrera_admindeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.carrera_patrocinador
    ADD CONSTRAINT carrera_patrocinador_nombrecarrera_admindeportista_fkey FOREIGN KEY (nombrecarrera, admindeportista) REFERENCES public.carrera(nombre, admindeportista) ON DELETE CASCADE;
 v   ALTER TABLE ONLY public.carrera_patrocinador DROP CONSTRAINT carrera_patrocinador_nombrecarrera_admindeportista_fkey;
       public          postgres    false    2928    213    213    201    201            �           2606    57333 A   carrera_patrocinador carrera_patrocinador_nombrepatrocinador_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.carrera_patrocinador
    ADD CONSTRAINT carrera_patrocinador_nombrepatrocinador_fkey FOREIGN KEY (nombrepatrocinador) REFERENCES public.patrocinador(nombrecomercial) ON DELETE CASCADE;
 k   ALTER TABLE ONLY public.carrera_patrocinador DROP CONSTRAINT carrera_patrocinador_nombrepatrocinador_fkey;
       public          postgres    false    213    203    2934            �           2606    57378 H   deportista_carrera deportista_carrera_nombrecarrera_admindeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.deportista_carrera
    ADD CONSTRAINT deportista_carrera_nombrecarrera_admindeportista_fkey FOREIGN KEY (nombrecarrera, admindeportista) REFERENCES public.carrera(nombre, admindeportista) ON DELETE CASCADE;
 r   ALTER TABLE ONLY public.deportista_carrera DROP CONSTRAINT deportista_carrera_nombrecarrera_admindeportista_fkey;
       public          postgres    false    217    2928    201    201    217            �           2606    57373 <   deportista_carrera deportista_carrera_usuariodeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.deportista_carrera
    ADD CONSTRAINT deportista_carrera_usuariodeportista_fkey FOREIGN KEY (usuariodeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;
 f   ALTER TABLE ONLY public.deportista_carrera DROP CONSTRAINT deportista_carrera_usuariodeportista_fkey;
       public          postgres    false    2924    217    200            �           2606    57278 *   deportista deportista_nombrecategoria_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.deportista
    ADD CONSTRAINT deportista_nombrecategoria_fkey FOREIGN KEY (nombrecategoria) REFERENCES public.categoria(nombre) ON DELETE CASCADE;
 T   ALTER TABLE ONLY public.deportista DROP CONSTRAINT deportista_nombrecategoria_fkey;
       public          postgres    false    200    2938    205            �           2606    57368 ?   deportista_reto deportista_reto_nombrereto_admindeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.deportista_reto
    ADD CONSTRAINT deportista_reto_nombrereto_admindeportista_fkey FOREIGN KEY (nombrereto, admindeportista) REFERENCES public.reto(nombre, admindeportista) ON DELETE CASCADE;
 i   ALTER TABLE ONLY public.deportista_reto DROP CONSTRAINT deportista_reto_nombrereto_admindeportista_fkey;
       public          postgres    false    218    2932    202    202    218            �           2606    57363 6   deportista_reto deportista_reto_usuariodeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.deportista_reto
    ADD CONSTRAINT deportista_reto_usuariodeportista_fkey FOREIGN KEY (usuariodeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;
 `   ALTER TABLE ONLY public.deportista_reto DROP CONSTRAINT deportista_reto_usuariodeportista_fkey;
       public          postgres    false    2924    200    218            �           2606    57273     grupo grupo_admindeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.grupo
    ADD CONSTRAINT grupo_admindeportista_fkey FOREIGN KEY (admindeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;
 J   ALTER TABLE ONLY public.grupo DROP CONSTRAINT grupo_admindeportista_fkey;
       public          postgres    false    200    208    2924            �           2606    57303 3   grupo_carrera grupo_carrera_idgrupo_admingrupo_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.grupo_carrera
    ADD CONSTRAINT grupo_carrera_idgrupo_admingrupo_fkey FOREIGN KEY (idgrupo, admingrupo) REFERENCES public.grupo(id, admindeportista) ON UPDATE CASCADE ON DELETE CASCADE;
 ]   ALTER TABLE ONLY public.grupo_carrera DROP CONSTRAINT grupo_carrera_idgrupo_admingrupo_fkey;
       public          postgres    false    2942    211    211    208    208            �           2606    57308 ;   grupo_carrera grupo_carrera_nombrecarrera_admincarrera_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.grupo_carrera
    ADD CONSTRAINT grupo_carrera_nombrecarrera_admincarrera_fkey FOREIGN KEY (nombrecarrera, admincarrera) REFERENCES public.carrera(nombre, admindeportista) ON DELETE CASCADE;
 e   ALTER TABLE ONLY public.grupo_carrera DROP CONSTRAINT grupo_carrera_nombrecarrera_admincarrera_fkey;
       public          postgres    false    201    211    211    201    2928            �           2606    57298 >   grupo_deportista grupo_deportista_idgrupo_admindeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.grupo_deportista
    ADD CONSTRAINT grupo_deportista_idgrupo_admindeportista_fkey FOREIGN KEY (idgrupo, admindeportista) REFERENCES public.grupo(id, admindeportista) ON UPDATE CASCADE ON DELETE CASCADE;
 h   ALTER TABLE ONLY public.grupo_deportista DROP CONSTRAINT grupo_deportista_idgrupo_admindeportista_fkey;
       public          postgres    false    210    208    208    210    2942            �           2606    57293 8   grupo_deportista grupo_deportista_usuariodeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.grupo_deportista
    ADD CONSTRAINT grupo_deportista_usuariodeportista_fkey FOREIGN KEY (usuariodeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;
 b   ALTER TABLE ONLY public.grupo_deportista DROP CONSTRAINT grupo_deportista_usuariodeportista_fkey;
       public          postgres    false    2924    200    210            �           2606    57313 -   grupo_reto grupo_reto_idgrupo_admingrupo_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.grupo_reto
    ADD CONSTRAINT grupo_reto_idgrupo_admingrupo_fkey FOREIGN KEY (idgrupo, admingrupo) REFERENCES public.grupo(id, admindeportista) ON UPDATE CASCADE ON DELETE CASCADE;
 W   ALTER TABLE ONLY public.grupo_reto DROP CONSTRAINT grupo_reto_idgrupo_admingrupo_fkey;
       public          postgres    false    2942    212    212    208    208            �           2606    57318 /   grupo_reto grupo_reto_nombrereto_adminreto_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.grupo_reto
    ADD CONSTRAINT grupo_reto_nombrereto_adminreto_fkey FOREIGN KEY (nombrereto, adminreto) REFERENCES public.reto(nombre, admindeportista) ON DELETE CASCADE;
 Y   ALTER TABLE ONLY public.grupo_reto DROP CONSTRAINT grupo_reto_nombrereto_adminreto_fkey;
       public          postgres    false    202    202    212    212    2932            �           2606    57263 7   inscripcion inscripcion_nombrecarrera_admincarrera_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.inscripcion
    ADD CONSTRAINT inscripcion_nombrecarrera_admincarrera_fkey FOREIGN KEY (nombrecarrera, admincarrera) REFERENCES public.carrera(nombre, admindeportista) ON DELETE CASCADE;
 a   ALTER TABLE ONLY public.inscripcion DROP CONSTRAINT inscripcion_nombrecarrera_admincarrera_fkey;
       public          postgres    false    204    2928    201    204    201            �           2606    57258 .   inscripcion inscripcion_usuariodeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.inscripcion
    ADD CONSTRAINT inscripcion_usuariodeportista_fkey FOREIGN KEY (usuariodeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;
 X   ALTER TABLE ONLY public.inscripcion DROP CONSTRAINT inscripcion_usuariodeportista_fkey;
       public          postgres    false    2924    204    200            �           2606    57253    reto reto_admindeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.reto
    ADD CONSTRAINT reto_admindeportista_fkey FOREIGN KEY (admindeportista) REFERENCES public.deportista(usuario) ON DELETE CASCADE;
 H   ALTER TABLE ONLY public.reto DROP CONSTRAINT reto_admindeportista_fkey;
       public          postgres    false    202    200    2924            �           2606    57343 ;   reto_patrocinador reto_patrocinador_nombrepatrocinador_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.reto_patrocinador
    ADD CONSTRAINT reto_patrocinador_nombrepatrocinador_fkey FOREIGN KEY (nombrepatrocinador) REFERENCES public.patrocinador(nombrecomercial) ON DELETE CASCADE;
 e   ALTER TABLE ONLY public.reto_patrocinador DROP CONSTRAINT reto_patrocinador_nombrepatrocinador_fkey;
       public          postgres    false    215    2934    203            �           2606    57348 C   reto_patrocinador reto_patrocinador_nombrereto_admindeportista_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.reto_patrocinador
    ADD CONSTRAINT reto_patrocinador_nombrereto_admindeportista_fkey FOREIGN KEY (nombrereto, admindeportista) REFERENCES public.reto(nombre, admindeportista) ON DELETE CASCADE;
 m   ALTER TABLE ONLY public.reto_patrocinador DROP CONSTRAINT reto_patrocinador_nombrereto_admindeportista_fkey;
       public          postgres    false    202    215    215    202    2932            5   �   x��ҿn� ���/�� FQe��1��=)n�c���>}!��*6�'�O�㒐I��$D>�Pz��0
�	�DJ�>��h�h=����{K�H]�g��A��[���@��E�,8|��
������&� �����n�,@����a�E���P��z���R�,��m��ɗ���W�).�4>�[t�T��:>l�9RU��֚�k�0��܆V"�z+23ʹ.�7>V�ˎs��?�      8   W   x�M�A
�0��cz�3^B	X�IIZQ_���vf�5,�u���	6v�;^�Q#�����;�㤓��^dHJV�+������/j      0   _   x�s�K)-J�KNU0202�,N��K,.)M�qu�t8c�8M8�3�s2�s�9K�B2R�R�K8����j��jM�j�R�8Ӹb���� î�      =   a   x��*���/�t�K)-J�KNU0202�,N��K,.)M�
.M�52�-��R�	�p"���
���<ΐ�T�����"s��P��6auF����� "�N�      ?   g   x��̱@0 й��@��uw+�b�44�0(�/e2���u�|�a�bAHhRXː�3��G��ע"�J,TA��+Ξk��K,��3��2��m��6V���% \ާ+�      <   X   x����q��t�K)-J�KNU0202�,N��K,.)M�r��/HI,*J�:���_�`ΐ�T�����"s.��҂|O?4�=... �1$�      4   �   x��*���/��M��/VHIU04UH<�1��+�4I�Ș*�`d�/H��� E����9�%��ɥ�9����E�
���
Pff^qrQfRfQq*�obqIj��#��f�	P'���!P�Mƙ3��B�M�21z\\\ ��?�      /      x��;�%�u��U`����L1A	"�:�$���;�=���4hȥ;��� Ā���u�2���,��7��/�������������+�^�|�����t���݇��ۗ}=?��?޾y?^�x{}y�?��a��o�����><�{�����ë�o��Oo_���^�������o�{�����z�߿飏�~r3{��+o,D��������~������~�����̝�o��u�?����~z����W!�~}����o^����/���ߞ~���~���?�÷�+o��/���q�������y����~�}՟������v�ݟ���+�"��+}�5��X��������O_������ۗ����y�m��~y{��:�M~���{y����s����������}<<ߟ����,#+�.���������ǧ�����?>�W�e�_>�ϧ��������z��-�w��������_��6��}���ߕߏ[��OnP_]Y���Ϗ���O/�'5�ۼ&��cP�{������jUr]&���x��?���+���}�/>��|.?�	���燗w��NP^>��?�c�_|���������BI��~���~��s��w���㧷/�n�d��?~
?���Sm٥��[������J-Քr���;����\���SS\����K�!��7������'�J�i%���7�4������Rͭ������~;�Z���XUJ>�R�ޫ-w��i��fg�vr�,>Z�yX]i坼�ɝEz��\t;�Ŝ��c�����c�m�|�z�����)��S>����E�N0�6}�)��.��yB�+��8-�%e_Rm��1����r���9�is6¶�Z�ͱ��Ke�3wn���,$*1��~��$��St��?�?��S��3����V��a�A�9��RK�5X��NW��|��]g�J�ˎ�o����%�(�T
)��S�}�B��W�'��/���\�/��	�8���������軝~H��_��h.�,�f�9Ӟ��xX���&%�|�k���c���N�V��y�򫬾���̒����[5ZK��S}[4@�&[�sL�h.�8�m>D��4s�y�CӖS(�rFZ!��#�ؑl#R����%sq��Z�~��[�*�=V�W�j����]������Y�h���9�*�a�,�P�eh[r9ٟnl���}f�ɽOG�2�Z��T'���,�+�8=f��h�uɖ,6���o���YIu��ely�)Lnk3�®����Qu��:�ma�6�;��a$^���r3,����O�+����t���=��JM�"c�*%����7X1v���=�@�����mM�C]|��8`�#q�@.�z�|	���I�K�[�cl@��=Y���#%��tv��:7���I�$?�&��	L�\��p'&��QG�*}��ªc����U���+fP�Q�i��i�������o(�l�Ձs�H#��R��W>�N�Q\�x+�b����+�]r.��g<�0p��&'������7����Vkۺ�J9�ԧ�����)���6�ǻ�d�ܱ�B@s����R�f�F�W��>�����q���T5� ��T}��L���ɪ8=����{؈��#����Fe�xR���4�2WC[�g�D ��},}��\�I�d�%�*���u�>,��R�AɵR�b��6v\a�d ���lu�S���V�`J��+�#��A�T��JA*9-2Ok����X�����o�E�t������ �& �ћ��u;�zDe��G�(�lЈ�ܬ����CrTu�P)\:���#z�P8�B��}A��4r} ]H�X-�vū~��-�qc%�bO-ہ�{�	;B��~1l��U:�@h;gM��(hAD��Da�I�
)N"6� ��m�t�T-K�9�oht-Y�l�4�
*�L� �͵��;�����^��Q骀��6,�H+W�aRO�"hM�ĘKYZaL�ΨlI5v�#um��k���"r:��B�B5���43��~ �x�(Y0喆�ECDB�Z,.#e~��C���������?F	�/���%�4!�{;L�$�*��O-����A�b.һ�s��$q*b�����z�_Q-�����Pct��½*����mHTsހGD��d@���:4"�<%�rt ��`6��Psp0��(BB���ЀȂi{�B24�Ƒ�٤�ER�RRS+W]C�N��'�a �i��H] �����C�}�7�GsS�˓�\2߇��m�6�P�$���"�7a��*��H`��r��V{>��Jb)Y��j�k�y(��Aլ�*���}��R�pV[�ڥm�H�	�D�Q��>h�c�P��%J^D�@/S�>��4�`O�TH�:�x��3���f�0t���dB�KDFp�`��|TK3�-6���8�%���g�9��@�ā�Gt�8E:�ye���Hy�U�����v^3�D��P�����#0t+9.��A4 ��o�jK7��l66�$�þQ�	T&�sIc��
�r��XM�p��\+�$���#\��
c"��XYUn�!���#��	��84t&���Qs�:D`��V`��
W M=%�N��/0݀�s$��w6���%F�YLL�6�e?L�� 4�G-��3�$e��\��h`�"0�_$�������|��(h齌�$�_>f�+��$?fr�F���f!X \3�ȫ�-i�����1�?���An�2:�cMV��P��^.��J�P�2��b��W#
���i-���Dy� t.��L�\��"&O�2���
�T#��^���b_���I>b�0֞�Іh�\˂�ThW���9�q�����C��\�V��gt0fP���^�����"A�	���U 3BJc����˸0�71S�HRMvD,���Mp�Yɴt]*\J��k7?��@���0@m��y��d����~D��C��8��G�,�,H��%� 脙\�x���(,��JZ���p7�b8���k��;�Up�iCS-v��:�]Z �11��A�Y�7C��=�.*��Ћx%��������iu�G�����l�<:&N� �C> �t ڙ-H7b�Y���n��r�c�B��qBT 5
{�������0����"6��|�йǡ��3�&�G�Ab&��@b'�k�N%���Θ���/�9�!�n�v��u�W��A�Q$����@8�1r��>̚A��RV=�sC\�(`�R��S�g�i>h8���s��qPT�cNK��X[��SQ���!�<;�E�&6�Aޔ1rn%�^<�*��bA�)����p��1�?��PX�|���X����@�8�^�3��j���S�-���C���PXc%w��0bƟrPҹ!���>"$کT�~��
� ]�Ј�&J�:ji�4O=T$VF�0gC�J����&��,�p� :��/����F��9T
�Os��#�eUۆ	�x'v0n8T��x�T��yi��쒵�zz����5'��mΠݦʝ�ḏw�eO��f@��o*O�A�a���:7њ�k߉ދ��Ơ���7�nt�̈@��2�"!{ jӬ���Sե�.�@S7�x��S�X�8
H�au� �N�����!6�;��A��Lt줯��'��BEO����0Zq�a�teMԐ���Ńkf59�@Rl�(��'ࢋ!u%Ϝe51��F��:��TƤ�Ì�ǭj���f���^Ӝ+	2�a�,�d0�4j�vOE-�_��"v��5)�Z,
��X	OTR���4�	r�?�{����w������L�sʟ� ���5a*�E"���5" �F�?���d�P �Y#|V�Գ��P�x�M��4MР�5��~�Y���X�8\���&C#�!P5��j�NIm��*� �� 2!;��k�H�͢&�vǽ�'�u���	8>ե���"��I�9sX9,G6}j�<J�(���wq���(>#D�J �  N�ϥ�]�!��ʫW��q>�F{��Q� uT b��L�ώ�k��&��u�A��S�lh�P�qQ#W�"(����� ]����X*/�A�7��֍IQ�24�Bl�.��6KJS���tY鎞O.`OSX���D��F,R�Q�1��IV���b~���>I��]�`h��k��nD��H��F9� �3��������x+H)�=�Cҳv��E <2�df�s�ȅ�K�/8t8�9=F��M���E\�����_�T �&��:1@3,XJ���m��ң���+~o��E��}w�w�J��)��e�CTb�H�M�L|t,�w�2m��� S����1���t�7#�\��[�.(��#���D��$�Qc4w�i
e~���1�Z�!��B�k4I((�U���$��;	F<y@��|�
2�=�+qfF� ��l���� xQ��k`�8�eȡl�]�!8C$=ڋ�!&�u��6�L�P�=ȗTRO���>4�b������(��#���B:|�Ԅ{��z��4e_;ޚ������BT�O���'��Bf�����k��7ۚ��k#tʐ������_�wh9��]��4*1���!�#�ƚ�Dq��E�N�%�}.�b�P�5!-��#�"�'����d֛��Q���i=�n/̀����8�wj���w@���A`8��v�1f(��+Q<46D���w&>���?fhʢ��8�j�
�Nm�Z��h^�6"��;`�<(��=a�	,Y�nBט��6~�qD��o|�J����ҰHI�͏-�a>���J���C�%#���� ��I��J��F��ļ�vB5d"�t��򺀵�U~�+P$�	s�s�jK��ytz��v�6
���]��hQ��Q�f�]4%���ьQ���1*�f^ Oҙ�$z�zn[u[t^��H�TP� ���O�:���(hi�G�	���G�.�*G9s:1С�!�0�5�������D�KC�%�\D]<%��DA'y�)�ɤ��F�6��n��<�8];axȜ�X�)$�DV�f�4�KD��(�.S����p:В�p:j�Z��Ob��u
:!L�@/�HM*D�f�Z�A�� 34�Fƭ�|C.�1�]��t
���j��@_���Ն|:��0������N�����{�*
�>5(���x+�0߰�Nk84>]h"�A��S+��_VF)�<Q`U���p�Y}Sth?�|��|��T񠆨+w���R#r�������F�o60���rsc��%[�M=h%9�!\����$c��O�ex��J��|���SV������q?�L��T�
@�"��j����H�FV5��D9Tr�,`)*�L'�-S+/�!�G�09G�g��N��"�R|��9#�uZX��{�L=�tyCo�Ʊa���,�j!��D�A��r���.K�3	x��M�s٣P�h���$['|-�������@����u��7��IŁ]hzjC=Y��\�_*�J��?�)H�4��q2��D@'��������rt\��(f�m���U���$Q�rK����C�;%ų4A��[En�K��d.��f{;I�tx�`�ș��Y�������a��^�8N�w��ŽQH֍V�t��������c/���������yp�Lsa	�W\|� ��b��&�V�Y���e�ƌ�*�d����Qb�1�m���/+�P#��N�W��ݔg��V�~klR�,/2��a�x[sT��O�� EH�"@D����c�ܴ������S���^G3�?�*�]�9A���|m�)"*���M�X�p$h:�.nW��<�L䄨��W�tƀ � C�F�=܍��%�ܵi�ft�$"�Η6L�7�2��QϤ�H�q���In���bK�-mM ��L�
�Y8�!O����#��w/�
;�vMGi�@#}~�BL�,�5�ee(�!�e�k��A���T}���Y�S42VL}dG�2 +2M���@(�y".�zlow�n�kh��^� SH ����lJ#�- e*Ptʳ��!)��&�ԋH���p���z��F8µQ�z	�G�Zi�$��1.�)�5���DA8VJ�a���燦��>N�����e�\�V��Q>h�	�g�վP%KxE�m��$ ����5}k���	���E 7$>�hO���y����>�QP�4r�vM�!=�Z�:�UQ�b�= �N�� ��'L�QS�D��+<
eP�\A(�Mѩ;��w�ڶcM{�zv:H�lq�^ȝ�#��k���Q
p2dL�CH�h0���9c�OȑE��`s�F-j��^��ߡ= :.�1s�fg>�#��²��&@z��z}�zOCo�ͦ�t�Τ)b�� �|,j��б�X��=[�9�'�i�b�t��h:��;���y�]��;
t�`B�:�YY�kcu��p�X?��(�"�+V�t�[�?����zk������otՄ�In�R�\"$+�5>G��b����IS&|P��F�W���A�C�<$��^���c:�Q��':8�]o����@0���%tX�� �I�a�#T��֠�E�Eg��s�O��]Ϋ�vv��|�9`��`fr ^��l%X�i���_s��~D&� ��/NB�ܝ*�цU����F��u��m'��4S; a�X�]�� �5�����m��MA=��'q��-�]�諎�5
`�tl�u�*�ip��i,8`:m��)�QAT����[s�.�<D�c�(�/�XR�	�:��
���T:O��>��&�)z��Q( h�� D;@��-��in֦y����\l�	��PF�P	������llJ�J�9P����J=�rI�:�/P!oTIӨL`�>�2Qj�
|;!���,O�w.&79e�T�^�)����ZX`��j1��U�����$۔C����&��˺��	�[��Ϩ^9}R�G�\'Q���&��)J#b��^^�rGb�B���������-��!�"*�e6;��T5�G�X.@��T���7�0~:>48�x���A]�D�TP�[o�@�.�.C���~%�C�Ua�����ad�,�č򹄷l��I:���RlPl���M/�&��>$z��se2�1�Ed?.�v�X�t���:^��P�5H�n9\�b�(q��^Q��L�|p��X�)Hͬ�Ƅ���1�}",�ტwYK�a4�Po���t��7[3���9��m���~hBNJ�zw:!�H;��ޭ�l��y!ל��e�k�u�R@��5����ߢ|`��-.�"A�z�z��~��/q�%�!-JwM����:X!���\�@K���Wx7�[�1�n�@R%���o��΋��1,;(=��Һ�*�Y��p�b�������kb.�ރKf�����cz���1_��z��I�ǖۂ?�1�ÜC�4���W��o�;�Q�h�rҬ�M�ͧ����Y�Gc<	��/�c���p	'�BQf$���췿����E�]A      @   �   x�}�1�0E��=AUuaG��,N0jK�D��
��,T)��O�`�Q��9�ddG�8���/P��cf�qmI}	h�>[�b�o���%GN��-LJBn2牺#����0o�\��e��+_��l�ի[~�\z xM�y�      A   M   x�K-I-NM��J-�W0��N-��+)ʯ�L�4�J��.�XZ��W��X	�6�,N��K,.)MK�W��%b������ �$      7   Y   x�3��I,Vp-.)J��I,�,N��K,.)M�2���/V�/�/��N-��+)ʯ�2�:e&g ���̹L�n��g&��R��b���� �(      :   8   x�s�K)-J�KNU0202�,N��K,.)MDbr��R��Z��WR�_�i����� ��      9   b   x�U�K
�0E�q���p3NB	X폦u���w����z���"Ʌ�m��M
W�ԔwA��������Φ��i�ݏM�#cKX�Y!�B�4�      ;   3   x�J-�W0�,N��K,.)M�L.2�4�
B�N-��+)ʯ�4����� 
f@      3   �   x��ѽ
�0�9y�>A)]�\����ښ?�#�ӛA�@������[���(�E̖Mv6s�0��q�{�����6~��j���6�����h��&�rA���I9r��#�?S3�%�.ԝ(D�+9E
$��� k� S�;/���b_����yt5�[=�z��EȾ�      2   �   x�u��N�0Eg�+<�P���P�B��������R'/zv+5�7�!��]�T�~��-x;�*�/b��o�� <s���ۢ^{�<}�Ȫ�Z�;R9[�*o�dg&�ε�_GY�$rA4�
�a��O{7�3^�b��{U�����\e�<K�|x����4�7�N��b���~�>�j��>�1ɘ(M����:��b�����%���6���K����LJ�m�`      1   �   x�m̱�0���<�H���:L��Ʊ/�ۇ��:��b���I��VR�z���OعF��EH '����`��J��e�{&�bq>J�"��Ϭ_��2����5�aI!�V�G�/��ĳZe��F��q�B�S�f��-�.���Fc�[�>^      >   Q   x����q��J-�W0��N-��+)ʯ�
q�u�v��/HI,*J�:�"m�Y����X\R���^TZ�����)���� �{#�     