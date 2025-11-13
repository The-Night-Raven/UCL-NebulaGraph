# UCL-NebulaGraph

Præsentation til [denne opgave](https://ucl.kjc.dk/dfu/dfu-exercises-other-databases.html)

## Præsentation
Start præsentation i VS Code med F1 + "revealjs: Open presentation in browser"

## NebulaGraph

Kør compose filen i `dockerz` mappen.

```sh
cd dockerz/
docker compose up -d
```

eller

```sh
cd dockerz/
podman-compose up -d
```

Det starter fire containere op:
- graphd
- storaged0
- metad0
- [web](http://localhost:7001)

Log på Studio med:

| Graphd IP address | Port | Username | Password |
|-------------------|------|----------|----------|
| graphd            | 9669 | root     | root     |

---

## Løst og fast

```sh
CREATE SPACE helloworld (VID_TYPE=FIXED_STRING(1));
# Host not enough!
# Ikke nok storage hosts (default replica_factor er 1, så vi har nul kørende)
# Tilføj med (https://docs.nebula-graph.io/3.8.0/4.deployment-and-installation/manage-storage-host/)

ADD HOSTS "storaged0":9779;

# Gentag CREATE

SHOW SPACES;
DESCRIBE SPACE helloworld;
```

```sh
MATCH (all_vertices) RETURN all_vertices; # Alle knuder
MATCH ()<-[all_edges]-() RETURN all_edges; # Alle kanter

```

---