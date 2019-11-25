package com.topcoder.quartzenergy.mudlog.search.service.impl;

import com.topcoder.quartzenergy.mudlog.search.dto.FilteredWell;
import com.topcoder.quartzenergy.mudlog.search.dto.RawPhraseData;
import com.topcoder.quartzenergy.mudlog.search.dto.WellPhrase;
import com.topcoder.quartzenergy.mudlog.search.dto.WellPhrases;
import com.topcoder.quartzenergy.mudlog.search.exception.MudlogException;
import com.topcoder.quartzenergy.mudlog.search.repository.WellRepository;
import com.topcoder.quartzenergy.mudlog.search.service.ExcelExportService;
import lombok.extern.slf4j.Slf4j;
import org.apache.poi.ss.usermodel.*;
import org.apache.poi.xssf.usermodel.XSSFWorkbook;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.servlet.http.HttpServletResponse;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.time.LocalDateTime;
import java.util.Date;
import java.util.List;

import static java.lang.Integer.MAX_VALUE;

/**
 * Implementation class for {@link ExcelExportService}
 */
@Service
@Slf4j
public class ExcelExportServiceImpl implements ExcelExportService {

    /**
     * the well repository instance
     */
    @Autowired
    private WellRepository repository;

    /**
     * header table columns for first sheet
     */
    private static String[] FIRST_SHEET_COLUMNS = new String[]{"Image Name", "UWI", "Phrase", "Count"};

    /**
     * header table columns for second sheet
     */
    private static String[] SECOND_SHEET_COLUMNS = new String[]{"IMAGE_URL", "IMAGE_NAME", "OCR_PHRASE_TYPE",
            "OCR_PHRASE", "SCORE", "X1", "Y1", "X2", "Y2", "ESTIMATED_DEPTH", "PHASE_KEY", "REF_KEY"};

    /**
     * Export filtered list of Well to excel
     *
     * @param response       the http response
     * @param uwis           list of UWI to search
     * @param states         list of states to search
     * @param basin          basin to search
     * @param county         county to search
     * @param field          field to search
     * @param minDepth       min depth of the well
     * @param maxDepth       max depth of the well
     * @param minVintage     min vintage year
     * @param maxVintage     max vintage year
     * @param phrases        list of phrases to search
     * @param minPhraseCount min phrase count
     * @param maxPhraseCount max phrase count
     * @param minPhraseScore min phrase score
     * @param maxPhraseScore max phrase score
     * @param minLat         latitude of top left corner of a rectangle
     * @param maxLat         latitude of bottom right corner of a rectangle
     * @param minLong        longitude of top left corner of a rectangle
     * @param maxLong        longitude of top bottom right of a rectangle
     * @return created excel byte array content
     * @throws MudlogException in case of any errors found while generating excel
     */
    @Override
    public byte[] export(HttpServletResponse response, List<String> uwis, List<String> states, String basin,
                         String county, String field, int minDepth, int maxDepth, int minVintage, int maxVintage,
                         List<String> phrases, int minPhraseCount, int maxPhraseCount, int minPhraseScore,
                         int maxPhraseScore, Double minLat, Double maxLat, Double minLong, Double maxLong)
            throws MudlogException {

        byte[] contentReturn = null;
        Workbook workbook = null;
        try {
            LocalDateTime localDate = LocalDateTime.now();
            String fileName = "phrases_report_" + new Date().getTime() + ".xlsx";
            response.setHeader("Content-Disposition", "attachment; filename=" + fileName);

            //get first sheet data
            List<FilteredWell> wellList = repository.searchWells(uwis, states, basin, county, field, minDepth,
                    maxDepth, minVintage, maxVintage, phrases, minPhraseCount, maxPhraseCount,
                    minPhraseScore, maxPhraseScore, minLat, maxLat, minLong, maxLong, 0, MAX_VALUE);

            //get second sheet data
            List<RawPhraseData> rawPhraseData = repository.searchAndGetRawPhraseData(uwis, states, basin, county, field, minDepth,
                    maxDepth, minVintage, maxVintage, phrases, minPhraseCount, maxPhraseCount,
                    minPhraseScore, maxPhraseScore, minLat, maxLat, minLong, maxLong);

            //write excel now
            workbook = writeExcelFile(wellList, rawPhraseData);

            OutputStream out = response.getOutputStream();
            workbook.write(out);

            ByteArrayOutputStream baos = new ByteArrayOutputStream();
            workbook.write(baos);

            contentReturn = baos.toByteArray();
            out.close();
            workbook.close();
        } catch (Exception ecx) {
            log.error("Export error", ecx);
            throw new MudlogException("Couldn't export wells", ecx);
        } finally {
            if (null != workbook) {
                try {
                    workbook.close();
                } catch (IOException eio) {
                    log.error("Error Occurred while exporting wells ", eio);
                }
            }
        }

        return contentReturn;
    }

    /**
     * Create Workbook with sheets holding well infos and phrases info
     *
     * @param records       the list of well info data
     * @param rawPhraseData the list of ocr phrase data
     * @return created Workbook
     */
    private Workbook writeExcelFile(List<FilteredWell> records, List<RawPhraseData> rawPhraseData) {
        Workbook workbook = new XSSFWorkbook(); // new HSSFWorkbook() for generating `.xls` file

        CellStyle headerCellStyle = getHeaderCellStyle(workbook);
        createPhrasesSheets(records, workbook, headerCellStyle);
        createRawPhraseDataSheets(rawPhraseData, workbook, headerCellStyle);

        return workbook;
    }

    /**
     * Create worksheet for well info data
     *
     * @param records         list of filtered well info
     * @param workbook        workbook the sheet would be added to
     * @param headerCellStyle header cell style
     */
    private void createPhrasesSheets(List<FilteredWell> records, Workbook workbook, CellStyle headerCellStyle) {
        log.info("Writing data into Phrase worksheet");
        // Create a Sheet
        Sheet sheet = workbook.createSheet("Phrases");

        // Create a Row
        Row headerRow = sheet.createRow(0);

        // Create header cells
        for (int i = 0; i < FIRST_SHEET_COLUMNS.length; i++) {
            Cell cell = headerRow.createCell(i);
            cell.setCellValue(FIRST_SHEET_COLUMNS[i]);
            cell.setCellStyle(headerCellStyle);
        }

        // Create Other rows and cells with employees data
        int rowNum = 1;
        for (FilteredWell each : records) {

            WellPhrases phrases = each.getPhrase();
            if(phrases.getPhrases().size() > 0) {
                boolean isFirstPhrase = true;
                for (WellPhrase eachPhrase : phrases.getPhrases()) {
                    Row row = sheet.createRow(rowNum++);
                    row.createCell(0).setCellValue(eachPhrase.getImageName());
                    row.createCell(1).setCellValue(each.getApi());
                    row.createCell(2).setCellValue(eachPhrase.getValue());
                    row.createCell(3).setCellValue(eachPhrase.getCount());
                }
            } else {
                Row row = sheet.createRow(rowNum++);
                row.createCell(0).setCellValue(each.getName());
                row.createCell(1).setCellValue(each.getApi());
            }

            if(rowNum/100 == 0) {
                log.debug("appended 100 rows...");
            }
        }
        log.info("Phrase worksheet created.");

        // Resize all columns to fit the content size
        for (int i = 0; i < FIRST_SHEET_COLUMNS.length; i++) {
            sheet.autoSizeColumn(i);
        }
    }

    /**
     * Create worksheet for well phrases info data
     * @param rawPhraseData list of phrases data
     * @param workbook workbook the sheet would be appended
     * @param headerCellStyle header cell style
     */
    private void createRawPhraseDataSheets(List<RawPhraseData> rawPhraseData, Workbook workbook, CellStyle headerCellStyle) {
        // Create a Sheet
        Sheet sheet = workbook.createSheet("Raw Phrase Data");
        log.info("Writing data into raw phrase data worksheet");

        // Create a Row
        Row headerRow = sheet.createRow(0);

        // Create header cells
        for (int i = 0; i < SECOND_SHEET_COLUMNS.length; i++) {
            Cell cell = headerRow.createCell(i);
            cell.setCellValue(SECOND_SHEET_COLUMNS[i]);
            cell.setCellStyle(headerCellStyle);
        }

        // Create Other rows and cells with phrase data
        int rowNum = 1;
        for (RawPhraseData each : rawPhraseData) {
            Row row = sheet.createRow(rowNum++);
            row.createCell(0).setCellValue(each.getImageUrl());
            row.createCell(1).setCellValue(each.getImageName());
            row.createCell(2).setCellValue(each.getPhraseType());
            row.createCell(3).setCellValue(each.getPhrase());
            row.createCell(4).setCellValue(each.getScore());
            row.createCell(5).setCellValue(each.getX1());
            row.createCell(6).setCellValue(each.getY1());
            row.createCell(7).setCellValue(each.getX2());
            row.createCell(8).setCellValue(each.getY2());
            row.createCell(9).setCellValue(each.getPhraseDepth());
            row.createCell(10).setCellValue(each.getPhaseKey());
            row.createCell(11).setCellValue(each.getRefKey());
            if(rowNum/100 == 0) {
                log.debug("appended 100 more rows...");
            }
        }

        // Resize all columns to fit the content size
        for (int i = 0; i < SECOND_SHEET_COLUMNS.length; i++) {
            sheet.autoSizeColumn(i);
        }
        log.info("Phrase raw data worksheet completed");
    }


    /**
     * Create and get style for header cells
     * @param workbook
     * @return
     */
    private CellStyle getHeaderCellStyle(Workbook workbook) {
        Font headerFont = workbook.createFont();
        headerFont.setBold(true);
        headerFont.setFontHeightInPoints((short) 14);
        headerFont.setBold(true);
        headerFont.setColor(IndexedColors.MAROON.getIndex());

        // Create a CellStyle with the font
        CellStyle headerCellStyle = workbook.createCellStyle();
        headerCellStyle.setFillForegroundColor(IndexedColors.GREY_25_PERCENT.getIndex());
        headerCellStyle.setFillPattern(FillPatternType.SOLID_FOREGROUND);
        headerCellStyle.setFont(headerFont);
        return headerCellStyle;
    }
}
