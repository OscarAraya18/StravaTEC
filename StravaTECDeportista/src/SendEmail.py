import smtplib

def recuperarClaveAcceso(correoEnviar, nombreUsuario, claveAcceso):
    to = 'osaronaragar15@gmail.com'
    gmail_user = 'StravaTEC@gmail.com'
    gmail_pwd = 'stravatec2020'
    smtpserver = smtplib.SMTP("smtp.gmail.com",587)
    smtpserver.ehlo()
    smtpserver.starttls()
    smtpserver.ehlo() # extra characters to permit edit
    smtpserver.login(gmail_user, gmail_pwd)
    header = 'To:' + to + '\n' + 'From: ' + gmail_user + '\n' + 'Subject:Recuperacion de Clave de Acceso de StravaTEC \n'
    print (header)
    msg = header + '\nEstimado @' + ', el equipo de StravaTEC le recuerda que su clave de acceso es ' + '.\n' + 'Ingrese a http://localhost:4200/ para continuar con su inicio de sesion. \n'
    smtpserver.sendmail(gmail_user, to, msg)
    print ('done!')
    smtpserver.quit()



if __name__ == "__main__":
    recuperarClaveAcceso("","","")
