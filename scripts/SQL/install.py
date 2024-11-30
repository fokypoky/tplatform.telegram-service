import psycopg2
from colorama import Fore, Back, Style, init
import configparser

print('- Reading SQL script')

script = ''
with open('./schema.sql') as file:
    script = file.read()

print('- SQL script is read')

print('- Reading database connection config')

config = configparser.ConfigParser()
config.read('./config.ini')

for section in config.sections():
    print(f'- Install for section {section}:')
    host = config[section]['Host']
    port = config[section]['Port']
    database = config[section]['Database']
    user = config[section]['User']
    password = config[section]['Password']
    print(f'* Host = {host}')
    print(f'* Port = {port}')
    print(f'* Database = {database}')
    print(f'* User = {user}')
    print(f'* Password = {password}')

    print('- Try to connect')
    connection = None
    try:
        connection = psycopg2.connect(
            host=host,
            port=port,
            database=database,
            user=user,
            password=password
        )
        print(Fore.GREEN + 'Connection established' + Style.RESET_ALL)
    except:
        print(Fore.RED + f"Can't to connect {section} section" + Style.RESET_ALL)
        continue

    print('- Executing...')
    
    try:
        with connection.cursor() as cursor:
            cursor.execute(script)
            print(Fore.GREEN + 'Done' + Style.RESET_ALL)
    except Exception as e:
        print(Fore.RED + f"Can't to execute script for {section} section. {str(e)}")