Quartz Server
=============

Server runs in Topshelf and listens on default port: 555

Server and client share a common job (Quartz.Jobs)

To run both server and client:

Solution properties
    - Multiple startup projects
    
    - Quartz.Server               (start)
    - Quartz.ClientScheduler      (start)


    