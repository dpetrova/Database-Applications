<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="/">
        <!--<xsl:copy>
            <xsl:apply-templates select="/"/>
        </xsl:copy>-->
      <html>
        <body>
          <h1>Catalog</h1>
          <table bgcolor ="#E0E0E0" cellspacing="1">
            <tr bgcolor ="#EEE">
              <td><b>Name</b></td>
              <td><b>Artist</b></td>
              <td><b>Year</b></td>
              <td><b>Producer</b></td>
              <td><b>Price</b></td>
              <td><b>Songs</b></td>
            </tr>
            <xsl:for-each select="/albums/album">
              <tr bgcolor="white">
                <td><xsl:value-of select="name"/></td>
                <td><xsl:value-of select="artist"/></td>
                <td><xsl:value-of select="year"/></td>
                <td><xsl:value-of select="producer"/></td>
                <td><xsl:value-of select="price"/></td>
                <td>
                  <xsl:for-each select="/songs">
                    <xsl:for-each select="/song">
                    <tr><td><xsl:attribute name="title"/></td></tr>
                    </xsl:for-each>
                  </xsl:for-each>
                </td>
              </tr>
            </xsl:for-each>
          </table>
        </body>
      </html>
    </xsl:template>
</xsl:stylesheet>
