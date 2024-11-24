import psycopg2
from colorama import Fore, Back, Style, init
import configparser

print('- Reading database connection config')

config = configparser.ConfigParser()
config.read('./config.ini')

host = config['Connection']['Host']
port = config['Connection']['Port']
database = config['Connection']['Database']
user = config['Connection']['User']
password = config['Connection']['Password']

print('-' * 50)

print(f'DATABASE CONNECTION CONFIG')
print(f'* Host = {host}')
print(f'* Port = {port}')
print(f'* Database = {database}')
print(f'* User = {user}')
print(f'* Password = {password}')

print('-' * 50)

print('- Try to connect PostgreSQL')
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
    print(Fore.RED + "Can't to connect PostgreSQL" + Style.RESET_ALL)
    exit(1)

print('- Reading SQL script')

script = ''
with open('./schema.sql') as file:
    script = file.read()

print('- SQL script is read')
print('- Executing...')

with connection.cursor() as cursor:
    cursor.execute(script)
    print(Fore.GREEN + 'Done' + Style.RESET_ALL)