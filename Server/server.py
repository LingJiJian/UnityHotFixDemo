#!/usr/bin/python
#coding:utf-8
if __name__ == '__main__':
	import socket
	sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
	sock.bind(('localhost', 8701))
	sock.listen(1024)

	print "[server] localhost:8701"
	
	while True:
		connection,address = sock.accept()
		try:
			buf = connection.recv(1024)
			if buf == '1':
				connection.send('welcome to server!')
			else:
				connection.send('please go out!')
		except socket.timeout:
			print 'time out'
		connection.close()