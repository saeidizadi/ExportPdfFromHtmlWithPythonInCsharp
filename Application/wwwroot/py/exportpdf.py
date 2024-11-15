import sys
import pdfkit

options = {
    'encoding': 'UTF-8',
}

html_file = sys.argv[1]
pdf_file = sys.argv[2]
config_path = sys.argv[3]
config = pdfkit.configuration(wkhtmltopdf=config_path)
pdfkit.from_string(html_file, pdf_file,options=options, configuration=config)
